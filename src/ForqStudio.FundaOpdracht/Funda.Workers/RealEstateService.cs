using Funda.Core.Application;
using Funda.Core.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Funda.Workers
{
    public class RealEstateService : IRealEstateService
    {
        private readonly ILogger _logger;
        private readonly IPartnerClient _partnerClient;

        public RealEstateService(ILogger<RealEstateService> logger, IPartnerClient partnerClient)
        {
            _logger = logger;
            _partnerClient = partnerClient;
        }

        public async Task<List<RealEstateObject>> GetAllRealEstateObjects(SearchQuery query)
        {
            var allFundaObjects = new List<RealEstateObject>();
            var hasNext = true;
            var currentPage = query.Page;
            while (hasNext)
            {
                Thread.Sleep(500); // TODO: Implement a delegating handler with a semaphore or create a PolicyWrap using Polly Library
                try
                {
                    if (currentPage != query.Page) //if we are not in the first iteration we need to update the query's page property
                        query.Page = currentPage;

                    var fetchResult = await _partnerClient.GetRealEstateSupplyAsync(query);

                    // if we have real estate objects we add them to the list
                    var noObjectsReturned = (fetchResult == null || fetchResult.Objects == null || !fetchResult.Objects.Any());
                    if (noObjectsReturned == false)
                        allFundaObjects.AddRange(fetchResult.Objects);

                    // if we do not have more items we set the hasNext flag to false and break out of the loop
                    if (fetchResult == null || fetchResult.Paging == null || fetchResult.Paging.VolgendeUrl == null)
                    {
                        hasNext = false;
                        break;
                    }

                    currentPage += 1;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            return allFundaObjects; // return the whole list so that we can use linq to filter and group
        }

        public List<RealEstateAgent> GetAgentsByObjectCount(List<RealEstateObject> realEstateObjects)
        {
            throw new NotImplementedException();
        }
    }
}

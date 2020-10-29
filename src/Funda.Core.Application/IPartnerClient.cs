using Funda.Core.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Funda.Core.Application
{
    public interface IPartnerClient
    {
        Task<GetAllObjectsResult> GetRealEstateSupplyAsync(SearchQuery query, CancellationToken cancellationToken);
    }
}

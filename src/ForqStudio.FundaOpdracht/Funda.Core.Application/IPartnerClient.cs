using Funda.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Funda.Core.Application
{
    public interface IPartnerClient
    {
        Task<GetObjectsResponse> GetRealEstateSupplyAsync(SearchQuery query);
    }
}

using Funda.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Funda.Core.Application
{
    public interface IRealEstateService
    {
        List<RealEstateAgent> GetAgentsByObjectCount(List<RealEstateObject> realEstateObjects);
        Task<List<RealEstateObject>> GetAllRealEstateObjects(SearchQuery query);
    }
}

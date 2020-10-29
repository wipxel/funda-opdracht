using Funda.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Funda.Core.Application
{
    public interface IRealEstateService
    {
        Task<List<RealEstateObject>> GetAllRealEstateObjects(SearchQuery query, CancellationToken cancellationToken);
    }
}

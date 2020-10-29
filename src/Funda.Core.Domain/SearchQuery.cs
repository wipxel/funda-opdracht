using System;

namespace Funda.Core.Domain
{
    public class SearchQuery
    {
        public string Type { get; set; }
        public string Location { get; set; }
        public bool HasGarden { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

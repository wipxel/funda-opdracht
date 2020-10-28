using Funda.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.Partners.API.Client
{
    // http://partnerapi.funda.nl/feeds/Aanbod.svc/[content-type]/[key]/?type=koop&zo=/amsterdam/tuin/&page=1&pagesize=25
    // TODO: this will get ugly fast because there are many more propeties to be added to SearchQuery object.
    public class QueryHelper
    {
        private const string FEED_AANBOD = "/feeds/Aanbod.svc/";
        private const string FEED_VRAAG = "/feeds/Vraag.svc/";
        private const string CONTENT_TYPE = "/json/";

        public static string BuildAanbodFeedQueryString(string apiKey, SearchQuery query)
        {
            var sb = new StringBuilder();
            var searchQuery = BuildSearchQuery(query);

            sb.Append(FEED_AANBOD);
            sb.Append(CONTENT_TYPE);
            sb.Append(apiKey);
            sb.Append(searchQuery);

            var queryString = sb.ToString();
            return queryString;
        }



        internal static string BuildSearchQuery(SearchQuery query)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(query.Type))
            {
                sb.Append("?type=");
                sb.Append(query.Type);
            }

            sb.Append("&zo=/");

            if (!string.IsNullOrEmpty(query.Location))
            {
                sb.Append(query.Location.ToLower());
                sb.Append("/");
            }

            if (query.HasGarden)
            {
                sb.Append("tuin/");
            }

            sb.Append("&page=");
            sb.Append(query.Page.ToString());
            sb.Append("&pagesize=");
            sb.Append(query.PageSize.ToString());

            return sb.ToString();
        }
    }
}

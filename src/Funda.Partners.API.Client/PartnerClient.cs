using Funda.Core.Application;
using Funda.Core.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Funda.Partners.API.Client
{
    public class PartnerClient : BaseClient, IPartnerClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public PartnerClient(HttpClient httpClient, string apiKey) : base(httpClient, apiKey)
        {
;            if (httpClient == null)
                throw new ArgumentNullException("httpClient");

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException("apiKey");

            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<GetObjectsResponse> GetRealEstateSupplyAsync(SearchQuery query, CancellationToken cancellationToken)
        {
            var searchQuery = QueryHelper.BuildAanbodFeedQueryString(_apiKey, query);

            try
            {
                var response = await _httpClient.GetAsync(searchQuery);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<GetObjectsResponse>(responseString, new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore }); ;
                    return responseObject;
                }

                return null;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }
    }
}

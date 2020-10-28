using System;
using System.Net.Http;

namespace Funda.Partners.API.Client
{
    public abstract class BaseClient
    {
        private readonly HttpClient _httpClient;
        private string _apiKey;
        
       public BaseClient(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }
    }
}

using SevenMediaAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SevenMediaAPI.Services
{
    public class TPAPIClientService : ITPAPIClientService
    {
        public static HttpClient httpClient;

        static TPAPIClientService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://f43qgubfhf.execute-api.ap-southeast-2.amazonaws.com");
        }

        public async Task<List<People>> ReadSample()
        {
            var contentResponse = await httpClient.GetAsync("sampletest");
            contentResponse.EnsureSuccessStatusCode();

            // Deserialize response
            var returnPeople = await contentResponse.Content.ReadAsAsync<List<People>>();
            return returnPeople;
        }
    }
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CLRClient.ServiceClients
{
    public class CustomerApiClient
    {
        private HttpClient client;

        public CustomerApiClient(string baseUri)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/customers/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Customer>(json);
        }
    }

    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

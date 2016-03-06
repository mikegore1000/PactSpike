using System.Collections.Generic;
using System.Threading.Tasks;
using CLRClient.ServiceClients;
using NUnit.Framework;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace CLRClient.Tests.ServiceClients
{
    public class CustomerApiClientTests
    {
        private CustomerApiClientPact pact;
        private IMockProviderService mockProviderService;


        [SetUp]
        public void SetUp()
        {
            pact = new CustomerApiClientPact();
            mockProviderService = pact.MockProviderService;
        }

        [TearDown]
        public void TearDown()
        {
            pact.Dispose();
        }

        [Test]
        public async Task Sample()
        {
            mockProviderService
                .Given("A customer with id '123'")
                .UponReceiving("A GET request")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/customers/123",
                    Headers = new Dictionary<string, string>
                    {
                        {"Accept", "application/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        id = 123,
                        firstName = "Mike",
                        lastName = "Gore"
                    }
                });

            var client = new CustomerApiClient(pact.MockProviderServiceBaseUri);
            var result = await client.GetCustomer(123);

            Assert.That(result.Id, Is.EqualTo(123));
            Assert.That(result.FirstName, Is.EqualTo("Mike"));
            Assert.That(result.LastName, Is.EqualTo("Gore"));

            pact.MockProviderService.VerifyInteractions();
        }
    }
}
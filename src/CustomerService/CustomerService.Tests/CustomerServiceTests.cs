using Microsoft.Owin.Testing;
using NUnit.Framework;
using PactNet;

namespace CustomerService.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        [Test]
        public void EnsureApiHonoursClrClientPact()
        {
            var pactVerifier = new PactVerifier(
                setUp: () => { },
                tearDown: () => { });

            pactVerifier.ProviderState("A customer with id '123'");

            using (var testServer = TestServer.Create<CustomerServiceStartup>())
            {
                pactVerifier
                    .ServiceProvider("Customer Service API", testServer.HttpClient)
                    .HonoursPactWith("CLR Client")
                    .PactUri("../../../../pacts/clr_client-customer_api.json")
                    .Verify();
            }
        }
    }
}

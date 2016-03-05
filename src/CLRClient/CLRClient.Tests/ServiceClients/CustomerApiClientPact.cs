using System;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace CLRClient.Tests.ServiceClients
{
    public class CustomerApiClientPact : IDisposable
    {
        private const int Port = 8080;

        private readonly PactBuilder pactBuilder;

        public IMockProviderService MockProviderService { get; }

        public string MockProviderServiceBaseUri => $"http://localhost:{Port}";

        public CustomerApiClientPact()
        {
            pactBuilder = new PactBuilder(new PactConfig
            {
                PactDir = "../../../../pacts"
            });

            pactBuilder
                .ServiceConsumer("CLR Client")
                .HasPactWith("Customer API");

            MockProviderService = pactBuilder.MockService(Port);
        }

        public void Dispose()
        {
            pactBuilder.Build();
        }
    }
}

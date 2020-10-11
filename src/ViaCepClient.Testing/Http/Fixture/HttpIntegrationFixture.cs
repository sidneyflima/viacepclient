using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace ViaCepClient.Testing.Http.Fixture
{
    [CollectionDefinition(nameof(ViaCepIntegrationFixtureCollection))]
    public class ViaCepIntegrationFixtureCollection: ICollectionFixture<HttpIntegrationFixture<ViaCepMockedStartup>> { }

    public class HttpIntegrationFixture<TStartup> : IDisposable 
    where TStartup: StartupBase
    {
        public IHost WebHost { get; }
        public HttpClient Client { get; }        
        public TStartup Startup { get; }

        public HttpIntegrationFixture() 
        {
            Startup = Activator.CreateInstance<TStartup>();
            WebHost = new HostBuilder()
                        .ConfigureWebHost(ConfigureWebHost)
                        .Start();
            Client = WebHost.GetTestClient();
        }

        private void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder .UseTestServer()
                    .UseEnvironment("Test")
                    .Configure(Startup.Configure)
                    .ConfigureServices(Startup.ConfigureServices);
        }

        public void Dispose()
        {
            Client.Dispose();
            WebHost.Dispose();   
        }
    }
}
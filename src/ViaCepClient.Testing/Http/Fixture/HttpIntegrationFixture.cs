using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
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

    /*
    public class HttpIntegrationAppFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return new HostBuilder()
                       .ConfigureWebHostDefaults(builder =>
                        {
                            builder.UseStartup<TStartup>()
                                   .UseTestServer();
                        });
        }
    }
    */

    public class ViaCepMockedStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseRouting();            
            app.UseEndpoints(routeBuilder => 
            {
                routeBuilder.MapGet("/ws/{cep}/json", context =>
                {
                    var routes = context.Request.RouteValues;

                    StringBuilder builder = new StringBuilder()
                        .Append("{")
                        .Append($@"""cep"":""{routes["cep"].ToString()}"",")
                        .Append($@"""logradouro"":""Praça da Sé"",")
                        .Append($@"""complemento"":""lado ímpar"",")
                        .Append($@"""bairro"":""Sé"",")
                        .Append($@"""localidade"":""São Paulo"",")
                        .Append($@"""uf"":""SP"",")
                        .Append($@"""ibge"":""3550308"",")
                        .Append($@"""gia"":""1004"",")
                        .Append($@"""ddd"":""11"",")
                        .Append($@"""siafi"":""7107""")
                        .Append("}");

                    return context.Response.WriteAsync(builder.ToString());
                });
            });
        }
    }
}
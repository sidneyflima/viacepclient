using System.Net;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ViaCepClient.Models;

namespace ViaCepClient.Testing.Http.Fixture
{
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
                    Cep cep = new Cep(context.Request.RouteValues["cep"].ToString());
                    cep.Validate();

                    if (cep.IsInvalid())
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "text/html";
                        return context.Response.WriteAsync("<html><head></head><body>400 - Bad Request</body></html>");
                    }

                    StringBuilder builder = new StringBuilder()
                        .Append("{")
                        .Append($@"""cep"":""{cep.Value}"",")
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

                    context.Response.StatusCode  = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync(builder.ToString());
                });
            });
        }
    }
}
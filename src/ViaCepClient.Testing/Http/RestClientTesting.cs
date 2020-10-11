using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using ViaCepClient.Http;
using ViaCepClient.Models;
using ViaCepClient.Testing.Http.Fixture;
using Xunit;

namespace ViaCepClient.Testing.Http
{
    [Collection(nameof(Fixture.ViaCepIntegrationFixtureCollection))]
    public class RestClientTesting
    {
        Fixture.HttpIntegrationFixture<ViaCepMockedStartup> _viaCepFixture;

        public RestClientTesting(Fixture.HttpIntegrationFixture<ViaCepMockedStartup> viaCepFixture)
        {
            _viaCepFixture = viaCepFixture;
        }

        [Fact]
        public async Task RestClient_ViacepCall_MustReturnResult()
        {
            //Given
            HttpClient  httpClient = _viaCepFixture.Client;
            IRestClient restClient = new RestClient(httpClient);
            
            string cep = "01001000";
            string expectedResponse = new StringBuilder()
                        .Append("{")
                        .Append($@"""cep"":""{cep}"",")
                        .Append($@"""logradouro"":""Praça da Sé"",")
                        .Append($@"""complemento"":""lado ímpar"",")
                        .Append($@"""bairro"":""Sé"",")
                        .Append($@"""localidade"":""São Paulo"",")
                        .Append($@"""uf"":""SP"",")
                        .Append($@"""ibge"":""3550308"",")
                        .Append($@"""gia"":""1004"",")
                        .Append($@"""ddd"":""11"",")
                        .Append($@"""siafi"":""7107""")
                        .Append("}")
                        .ToString();

            Uri uri = new Uri($"http://localhost/ws/{cep}/json/");

            //When
            RestResponse response       = await restClient.GetAsync(uri);
            Stream responseStream       = await response.GetResponseStream();

            using StreamReader reader = new StreamReader(responseStream);
            string actualResponse     = await reader.ReadToEndAsync();

            //Then
            response.Should().NotBeNull();
            response.Method.Should().BeEquivalentTo(HttpMethod.Get);
            response.IsSuccessfulResponse.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Uri.Should().Be(uri);

            actualResponse.Should().NotBeNull().And.Be(expectedResponse);
        }
    }
}
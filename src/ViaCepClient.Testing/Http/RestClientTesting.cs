using FluentAssertions;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ViaCepClient.Converter;
using ViaCepClient.Http;
using ViaCepClient.Testing.Fixture;
using Xunit;

namespace ViaCepClient.Testing.Http
{
    [Collection(nameof(ViaCepIntegrationFixtureCollection))]
    public class RestClientTesting
    {
        HttpIntegrationFixture<ViaCepMockedStartup> _viaCepFixture;

        public RestClientTesting(HttpIntegrationFixture<ViaCepMockedStartup> viaCepFixture)
        {
            _viaCepFixture = viaCepFixture;
        }

        [Fact]
        public async Task RestClient_ViacepCallInvalidCep_MustReturnBadRequest()
        {
            //Given
            HttpClient  httpClient = _viaCepFixture.Client;
            IRestClient restClient = new RestClient(httpClient);

            string cep  = "12";
            Uri uri     = new Uri($"http://viacep.com.br/ws/{cep}/json/");

            //When
            RestResponse response = await restClient.GetAsync(uri);

            //Then
            response.Should().NotBeNull();
            response.Method.Should().BeEquivalentTo(HttpMethod.Get);
            response.IsSuccessfulResponse.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Uri.Should().Be(uri);
        }

        [Fact]
        public async Task RestClient_ViacepCall_MustReturnOk()
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

            Uri uri = new Uri($"http://viacep.com.br/ws/{cep}/json/");

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

        [Fact]
        public async Task RestClient_ViacepCall_MustDeserializeJson()
        {
            //Given
            HttpClient httpClient = _viaCepFixture.Client;
            IRestClient restClient = new RestClient(httpClient);

            string cep              = "01001000";
            string address          = "Praça da Sé";
            string complement       = "lado ímpar";
            string neighbourhood    = "Sé";
            string city             = "São Paulo";
            string federativeUnit   = "SP";
            string ibge             = "3550308";
            string gia              = "1004";
            string ddd              = "11";
            string siafi            = "7107";

            Uri uri = new Uri($"http://viacep.com.br/ws/{cep}/json/");

            RestResponse response = await restClient.GetAsync(uri);

            //When
            using JsonDocument jsonDocument  = await response.AsJsonDocumentAsync();
            IPlainJsonObject plainJsonObject = new PlainJsonObject(jsonDocument);

            //Then
            plainJsonObject["cep"]          .Should().Be(cep);
            plainJsonObject["logradouro"]   .Should().Be(address);
            plainJsonObject["complemento"]  .Should().Be(complement);
            plainJsonObject["bairro"]       .Should().Be(neighbourhood);
            plainJsonObject["localidade"]   .Should().Be(city);
            plainJsonObject["uf"]           .Should().Be(federativeUnit);
            plainJsonObject["ibge"]         .Should().Be(ibge);
            plainJsonObject["gia"]          .Should().Be(gia);
            plainJsonObject["ddd"]          .Should().Be(ddd);
            plainJsonObject["siafi"]        .Should().Be(siafi);
        }
    }
}
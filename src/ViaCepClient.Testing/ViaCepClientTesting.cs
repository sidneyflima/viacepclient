using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ViaCepClient.Client;
using ViaCepClient.Http;
using ViaCepClient.Messages;
using ViaCepClient.Models;
using ViaCepClient.Testing.Fixture;
using ViaCepClient.Validators;
using Xunit;

namespace ViaCepClient.Testing
{
    [Collection(nameof(ViaCepIntegrationFixtureCollection))]
    public class ViaCepClientTesting
    {
        HttpIntegrationFixture<ViaCepMockedStartup> _viaCepFixture;

        public ViaCepClientTesting(HttpIntegrationFixture<ViaCepMockedStartup> viaCepFixture)
        {
            _viaCepFixture = viaCepFixture;
        }

        [Theory]
        [InlineData("https://viacep.com.br"   , "https://viacep.com.br")]
        [InlineData("http://viacep.com.br"    , "http://viacep.com.br")]
        [InlineData("https://viacep.com.br/ws", "https://viacep.com.br")]
        [InlineData("https://viacep.com.br/ws/01001000/json/", "https://viacep.com.br")]
        [InlineData("http://viacep.com.br/ws/01001000/json/", "http://viacep.com.br")]
        public void ViaCepClientOptions_WhenSetBaseAddress_MustRemoveRelativePath(string baseAddress, string expectedResult)
        {
            // Given
            ViaCepClientOptions options = new ViaCepClientOptions();

            // When
            options.SetBaseAddress(baseAddress);

            // Then
            options.BaseAddress.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("01001000", "https://viacep.com.br/ws/01001000/json/unicode")]
        [InlineData("12456789", "https://viacep.com.br/ws/12456789/json/unicode")]
        public void ViaCepRequestBuilder_WhenBuildingCepRequest_MustBuildExpectedUri(string cep, string expectedUrlAddress)
        {
            // Given
            ViaCepClientOptions   options = new ViaCepClientOptions();
            IViaCepRequestBuilder builder = new ViaCepRequestBuilder(options);

            Uri expectedUri = new Uri(expectedUrlAddress);

            // When
            Uri actualUri = builder.CreateRequestUri(new Cep(cep));

            // Then
            actualUri.Should().Be(expectedUri);
        }

        [Fact]
        public async Task SendRequestCep_WhenCepIsNull_MustReturnCepRequiredError()
        {
            //Given
            IViaCepClient viaCepClient = CreateViaCepClientInstance();

            //When
            ResponseMessage<CepDetails> response = await viaCepClient.SendRequestAsync(null);

            //Then
            response.Should().NotBeNull();
            response.IsSuccessfulResponse.Should().BeFalse();
            response.Content.Should().BeNull();
            response.ErrorCode.Should().Be(ErrorCodes.CepRequired);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12-34-45-32")]
        public async Task SendRequestCep_WhenCepIsInvalid_MustReturnValidationError(string cep)
        {
            //Given
            IViaCepClient viaCepClient = CreateViaCepClientInstance();

            Cep invalidCep = new Cep(cep);
            invalidCep.Validate();

            IError error = invalidCep.GetValidationErrors().First();

            //When
            ResponseMessage<CepDetails> response = await viaCepClient.SendRequestAsync(invalidCep);

            //Then
            response.Should().NotBeNull();
            response.IsSuccessfulResponse.Should().BeFalse();
            response.Content.Should().BeNull();

            response.ErrorCode.Should().Be(error.ErrorCode);
            response.ErrorMessage.Should().Be(error.ErrorMessage);
            response.ErrorPropertyName.Should().Be(error.PropertyName);
        }

        [Fact]
        public async Task SendRequestCep_WhenCepNotFound_MustReturnNotFoundError()
        {
            // Given
            IViaCepClient viaCepClient = CreateViaCepClientInstance();
            Cep notFoundCep = new Cep("00000000");

            // When
            ResponseMessage<CepDetails> response = await viaCepClient.SendRequestAsync(notFoundCep);

            // Then
            response.Should().NotBeNull();
            response.IsSuccessfulResponse.Should().BeFalse();
            response.Content.Should().BeNull();

            response.ErrorCode.Should().Be(ErrorCodes.ResourceNotFound);
        }

        [Fact]
        public async Task SendRequestCep_WhenCepFound_MustReturnCepDetails()
        {
            // Given
            IViaCepClient viaCepClient = CreateViaCepClientInstance();
            
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

            Cep validCep = new Cep(cep);

            // When
            ResponseMessage<CepDetails> response = await viaCepClient.SendRequestAsync(validCep);

            // Then
            response.Should().NotBeNull();
            response.IsSuccessfulResponse.Should().BeTrue();
            response.Content.Should().NotBeNull();

            response.ErrorCode.Should().BeNullOrEmpty();
            response.ErrorMessage.Should().BeNullOrEmpty();
            response.ErrorPropertyName.Should().BeNullOrEmpty();

            response.Content.Address.Should().Be(address);
            response.Content.Cep.Should().Be(cep);
            response.Content.City.Should().Be(city);
            response.Content.Complement.Should().Be(complement);
            response.Content.DDD.Should().Be(ddd);
            response.Content.FederativeUnit.Should().Be(federativeUnit);
            response.Content.GIA.Should().Be(gia);
            response.Content.IBGE.Should().Be(ibge);
            response.Content.Neighbourhood.Should().Be(neighbourhood);
            response.Content.Siafi.Should().Be(siafi);
        }

        private IViaCepClient CreateViaCepClientInstance()
        {
            var httpClient   = _viaCepFixture.Client;
            var options      = new ViaCepClientOptions();
            var builder      = new ViaCepRequestBuilder(options);
            var restClient   = new RestClient(httpClient);
            var viaCepClient = new Client.ViaCepClient(restClient, builder);

            return viaCepClient;
        }

    }
}

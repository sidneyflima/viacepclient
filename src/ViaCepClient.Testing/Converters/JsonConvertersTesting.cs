using System.Text;
using System.Text.Json;
using FluentAssertions;
using ViaCepClient.Converter;
using ViaCepClient.Models;
using Xunit;

namespace ViaCepClient.Testing.Converters
{
    public class JsonConvertersTesting
    {
        [Fact]
        public void PlainObject_WhenValidKeys_MustReadAllValues()
        {
            //Given
            string aKey = "a";
            string bKey = "b";
            string expectedAValue = "1";
            string expectedBValue = "2";
            string nullKey = "nullKey";

            string expectedJson = $@"{{""{aKey}"":""{expectedAValue}"",""{bKey}"":""{expectedBValue}"", ""{nullKey}"": null}}";

            using JsonDocument jsonDocument = JsonDocument.Parse(expectedJson);
            
            //When
            PlainJsonObject jsonObject = new PlainJsonObject(jsonDocument);
            string aValue = jsonObject[aKey];
            string bValue = jsonObject[bKey];

            string nullValue = jsonObject[nullKey];

            //Then
            aValue.Should().NotBeNull().And.Be(expectedAValue);
            bValue.Should().NotBeNull().And.Be(expectedBValue);
            nullValue.Should().BeNull();
        }

        [Fact]
        public void PlainObject_WhenKeyNotExists_MustReturnNull()
        {
            //Given
            string expectedJson = $@"{{""a"":""1""}}";
            using JsonDocument jsonDocument = JsonDocument.Parse(expectedJson);
            
            //When
            PlainJsonObject jsonObject = new PlainJsonObject(jsonDocument);
            string aValue = jsonObject["b"];

            //Then
            aValue.Should().BeNull();
        }

        [Fact]
        public void CepDetailsConverter_MustDeserializeAllFields()
        {
            //Given
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

            string json = new StringBuilder()
                            .Append("{")
                            .Append($@"""cep"":""{cep}"",")
                            .Append($@"""logradouro"":""{address}"",")
                            .Append($@"""complemento"":""{complement}"",")
                            .Append($@"""bairro"":""{neighbourhood}"",")
                            .Append($@"""localidade"":""{city}"",")
                            .Append($@"""uf"":""{federativeUnit}"",")
                            .Append($@"""ibge"":""{ibge}"",")
                            .Append($@"""gia"":""{gia}"",")
                            .Append($@"""ddd"":""{ddd}"",")
                            .Append($@"""siafi"":""{siafi}""")
                            .Append("}")
                            .ToString();

            CepDetailsConverter converter       = new CepDetailsConverter();
            
            using JsonDocument  jsonDocument    = JsonDocument.Parse(json);
            IPlainJsonObject    plainJsonObject = new PlainJsonObject(jsonDocument);

            //When
            CepDetails cepDetails = converter.FromJson(plainJsonObject);

            //Then
            cepDetails.Address.Should().Be(address);
            cepDetails.Cep.Should().Be(cep);
            cepDetails.City.Should().Be(city);
            cepDetails.Complement.Should().Be(complement);
            cepDetails.DDD.Should().Be(ddd);
            cepDetails.FederativeUnit.Should().Be(federativeUnit);
            cepDetails.GIA.Should().Be(gia);
            cepDetails.IBGE.Should().Be(ibge);
            cepDetails.Neighbourhood.Should().Be(neighbourhood);
            cepDetails.Siafi.Should().Be(siafi);
        }
    }
}
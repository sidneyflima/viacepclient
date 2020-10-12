using FluentAssertions;
using ViaCepClient.Messages;
using ViaCepClient.Models;
using Xunit;

namespace ViaCepClient.Testing.Models
{
    public class AddressUnitTesting
    {
        [Fact]
        public void Address_ValidateRequired()
        {
            //Given
            Address address = new Address("", "", "");
            
            //When
            address.Validate();
            
            //Then
            address.IsValid().Should().BeFalse();
            address.IsInvalid().Should().BeTrue();
            
            address.GetValidationErrors()
                .Should()
                .Contain(err => err.PropertyName == nameof(Address.City)                 && err.ErrorCode == ErrorCodes.CityRequired).And
                .Contain(err => err.PropertyName == nameof(Address.StateAbbreviation)    && err.ErrorCode == ErrorCodes.StateAbbreviationRequired).And
                .Contain(err => err.PropertyName == nameof(Address.StreetName)           && err.ErrorCode == ErrorCodes.StreetNameRequired);

        }

        [Fact]
        public void Address_ValidateLength()
        {
            //Given
            Address address = new Address("A", "ci", "st");
            
            //When
            address.Validate();
            
            //Then
            address.IsValid().Should().BeFalse();
            address.IsInvalid().Should().BeTrue();
            
            address.GetValidationErrors()
                .Should()
                .NotContain(err => err.PropertyName == nameof(Address.City)                 && err.ErrorCode == ErrorCodes.CityRequired).And
                .NotContain(err => err.PropertyName == nameof(Address.StateAbbreviation)    && err.ErrorCode == ErrorCodes.StateAbbreviationRequired).And
                .NotContain(err => err.PropertyName == nameof(Address.StreetName)           && err.ErrorCode == ErrorCodes.StreetNameRequired);
            
            address.GetValidationErrors()
                .Should()
                .Contain(err => err.PropertyName == nameof(Address.City)                 && err.ErrorCode == ErrorCodes.CityInvalidLength).And
                .Contain(err => err.PropertyName == nameof(Address.StateAbbreviation)    && err.ErrorCode == ErrorCodes.StateAbbreviationInvalidLength).And
                .Contain(err => err.PropertyName == nameof(Address.StreetName)           && err.ErrorCode == ErrorCodes.StreetNameInvalidLength);
        }

        [Fact]
        public void TestName()
        {
            //Given
            Address address = new Address("SP", "São Paulo", "Avenida Paulista");
            
            //When
            address.Validate();

            //Then
            address.IsValid().Should().BeTrue();
            address.GetValidationErrors().Should().BeEmpty();
        }
    }
}

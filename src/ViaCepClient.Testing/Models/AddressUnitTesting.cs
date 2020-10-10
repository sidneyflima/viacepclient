using FluentAssertions;
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
                .Contain(err => err.PropertyName == nameof(Address.City)                 && err.ErrorCode == Address.Errors.CityRequired).And
                .Contain(err => err.PropertyName == nameof(Address.StateAbbreviation)    && err.ErrorCode == Address.Errors.StateAbbreviationRequired).And
                .Contain(err => err.PropertyName == nameof(Address.StreetName)           && err.ErrorCode == Address.Errors.StreetNameRequired);

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
                .NotContain(err => err.PropertyName == nameof(Address.City)                 && err.ErrorCode == Address.Errors.CityRequired).And
                .NotContain(err => err.PropertyName == nameof(Address.StateAbbreviation)    && err.ErrorCode == Address.Errors.StateAbbreviationRequired).And
                .NotContain(err => err.PropertyName == nameof(Address.StreetName)           && err.ErrorCode == Address.Errors.StreetNameRequired);
            
            address.GetValidationErrors()
                .Should()
                .Contain(err => err.PropertyName == nameof(Address.City)                 && err.ErrorCode == Address.Errors.CityInvalidLength).And
                .Contain(err => err.PropertyName == nameof(Address.StateAbbreviation)    && err.ErrorCode == Address.Errors.StateAbbreviationInvalidLength).And
                .Contain(err => err.PropertyName == nameof(Address.StreetName)           && err.ErrorCode == Address.Errors.StreetNameInvalidLength);
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

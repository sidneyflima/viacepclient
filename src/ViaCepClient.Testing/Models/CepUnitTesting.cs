﻿using FluentAssertions;
using ViaCepClient.Messages;
using ViaCepClient.Models;
using Xunit;

namespace ViaCepClient.Testing.Models
{
    public class CepUnitTesting
    {
        [Theory]
        [InlineData("12345678", false, 1, 2, 3, 4, 5, 678)]
        [InlineData("12345-678", true, 1, 2, 3, 4, 5, 678)]
        public void TestValidCep(string cepString, bool hasHyphen, int region, int subregion, int sector, int subsector, int subsectorDivisor, int distribuitionSuffix)
        {
            Cep cep = new Cep(cepString);

            cep.IsValid().Should().BeTrue();
            cep.HasHyphen.Should().Be(hasHyphen);

            cep.GetRegion().Should().Be(region);
            cep.GetSubregion().Should().Be(subregion);
            cep.GetSector().Should().Be(sector);
            cep.GetSubSector().Should().Be(subsector);
            cep.GetSubsectorDivisor().Should().Be(subsectorDivisor);
            cep.GetDistribuitionSuffix().Should().Be(distribuitionSuffix);
        }

        [Fact]
        public void TestEmptyCep()
        {
            Cep cep = new Cep("");

            cep.IsValid().Should().BeFalse();
            cep.HasHyphen.Should().BeFalse();

            cep.GetValidationErrors().Should().NotBeEmpty();
            cep.GetValidationErrors().Should().Contain(err => err.PropertyName == nameof(Cep.Value) && err.ErrorCode == ErrorCodes.CepRequired);
            cep.GetValidationErrors().Should().Contain(err => err.PropertyName == nameof(Cep.Value) && err.ErrorCode == ErrorCodes.CepInvalidPattern);

            AssertInvalidCepComponents(cep);
        }

        [Fact]
        public void TestInvalidCep()
        {
            Cep cep = new Cep("234-5");

            cep.IsValid().Should().BeFalse();

            cep.GetValidationErrors().Should().NotBeEmpty();
            cep.GetValidationErrors().Should().NotContain(err => err.PropertyName == nameof(Cep.Value) && err.ErrorCode == ErrorCodes.CepRequired);
            cep.GetValidationErrors().Should().Contain   (err => err.PropertyName == nameof(Cep.Value) && err.ErrorCode == ErrorCodes.CepInvalidPattern);

            AssertInvalidCepComponents(cep);
        }

        private void AssertInvalidCepComponents(Cep cep)
        {
            cep.GetRegion               ().Should().Be(Cep.InvalidCepComponent);
            cep.GetSubregion            ().Should().Be(Cep.InvalidCepComponent);
            cep.GetSector               ().Should().Be(Cep.InvalidCepComponent);
            cep.GetSubSector            ().Should().Be(Cep.InvalidCepComponent);
            cep.GetSubsectorDivisor     ().Should().Be(Cep.InvalidCepComponent);
            cep.GetDistribuitionSuffix  ().Should().Be(Cep.InvalidCepComponent);
        }
    }
}

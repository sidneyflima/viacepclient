using System;
using ViaCepClient.Validators;

namespace ViaCepClient.Models
{
    /// <summary>
    /// Cep represents a cep value
    /// </summary>
    public sealed class Cep : ValidatableModel<Cep>
    {
        /// <summary>
        /// It is used to check if a cep component is valid
        /// </summary>
        public static readonly int InvalidCepComponent = -1;

        /// <summary>
        /// Cep value
        /// </summary>
        public string CepValue { get; }

        /// <summary>
        /// If cep contains a hyphen
        /// </summary>
        public bool HasHyphen { get; }

        /// <summary>
        /// Cep represents a cep value
        /// </summary>
        public Cep(string cep)
        {
            CepValue  = cep;
            HasHyphen = cep?.Contains('-') ?? false;
        }

        /// <summary>
        /// Performs model validation. A model is invalid if has any error
        /// after a validation. Therefore, if there is any invalid
        /// model property, the implementation must specify a error by
        /// adding a new error using 'AddError' method
        /// </summary>
        protected override void PerformValidation()
        {
            if (!CepValue.NotNullOrEmpty())
                AddError(nameof(CepValue), Errors.CepRequired, "Cep is required");

            if (!CepValue.RegexMatches(@"^(\d{5}\-\d{3})|(\d{8})$"))
                AddError(nameof(CepValue), Errors.CepInvalidPattern, "Cep pattern is required");
        }

        /// <summary>
        /// Cep region (X0000-000)
        /// </summary>
        public int GetRegion()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return CepValue[0] - '0';
        }

        /// <summary>
        /// Cep subregion (0X000-000)
        /// </summary>
        public int GetSubregion()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return CepValue[1] - '0';
        }

        /// <summary>
        /// Cep sector (00X00-000)
        /// </summary>
        public int GetSector()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return CepValue[2] - '0';
        }

        /// <summary>
        /// Cep subsector (000X0-000)
        /// </summary>
        public int GetSubSector()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return CepValue[3] - '0';
        }

        /// <summary>
        /// Cep subsector divisor (0000X-000)
        /// </summary>
        public int GetSubsectorDivisor()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return CepValue[4] - '0';
        }

        /// <summary>
        /// Cep distribuition suffix (00000-XXX)
        /// </summary>
        public int GetDistribuitionSuffix()
        {
            if (IsInvalid())
                return InvalidCepComponent;
            
            if (HasHyphen)
                return int.Parse(CepValue.Substring(6));

            return int.Parse(CepValue.Substring(5));
        }

        /// <summary>
        /// Converts a Cep to a string representation
        /// </summary>
        public override string ToString()
        {
            return CepValue;
        }

        /// <summary>
        /// Error codes
        /// </summary>
        public static class Errors
        {
            /// <summary>
            /// Error code when cep is required
            /// </summary>
            public static readonly string CepRequired = "CEP_REQUIRED";

            /// <summary>
            /// Error code when cep has invalid pattern
            /// </summary>
            public static readonly string CepInvalidPattern = "CEP_INVALID_PATTERN";
        }
    }
}
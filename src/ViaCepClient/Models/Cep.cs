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
        public string Value { get; }

        /// <summary>
        /// Cep represents a cep value
        /// </summary>
        public Cep(string cep)
        {
            Value = cep;
        }

        /// <summary>
        /// Cep region (X0000-000)
        /// </summary>
        public int GetRegion()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return '0' - Value[0];
        }

        /// <summary>
        /// Cep subregion (0X000-000)
        /// </summary>
        public int GetSubregion()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return '0' - Value [1];
        }

        /// <summary>
        /// Cep sector (00X00-000)
        /// </summary>
        public int GetSector()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return '0' - Value[2];
        }

        /// <summary>
        /// Cep subsector (000X0-000)
        /// </summary>
        public int GetSubSector()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return '0' - Value[3];
        }

        /// <summary>
        /// Cep subsector divisor (0000X-000)
        /// </summary>
        public int GetSubsectorDivisor()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return '0' - Value[4];
        }

        /// <summary>
        /// Cep distribuition suffix (00000-XXX)
        /// </summary>
        public int GetDistribuitionSuffix()
        {
            if (IsInvalid())
                return InvalidCepComponent;

            return int.Parse(Value.Substring(6));
        }

        /// <summary>
        /// Converts a Cep to a string representation
        /// </summary>
        public override string ToString()
        {
            return Value;
        }
    }
}
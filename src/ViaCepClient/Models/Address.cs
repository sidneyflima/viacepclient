using ViaCepClient.Validators;

namespace ViaCepClient.Models
{
    /// <summary>
    /// Address represents informations related to an 
    /// address in order to query related cep information
    /// </summary>
    public sealed class Address: ValidatableModel<Address>
    {
        /// <summary>
        /// StateAbbreviation represents initials which represents a state. 
        /// For instance, SP for São Paulo or RJ for Rio de Janeiro
        /// </summary>
        public string StateAbbreviation { get; private set; }

        /// <summary>
        /// City represents a city, which should be a city located 
        /// at state represented by StateAbbreviation property
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// StreetName represents a street name, which should be a 
        /// street located at city represented by City property
        /// </summary>
        /// <value></value>
        public string StreetName { get; private set; }

        /// <summary>
        /// Address represents informations related to an 
        /// address in order to query related cep information
        /// </summary>
        public Address(string stateAbbreviation, string city, string streetName)
        {
            StateAbbreviation = stateAbbreviation;
            City              = city;
            StreetName        = streetName;
        }

        /// <summary>
        /// Performs model validation. A model is invalid if has any error
        /// after a validation. Therefore, if there is any invalid
        /// model property, the implementation must specify a error by
        /// adding a new error using 'AddError' method
        /// </summary>
        protected override void PerformValidation()
        {
            if (!StateAbbreviation.NotNullOrEmpty())
                AddError(nameof(StateAbbreviation), Errors.StateAbbreviationRequired, "State Abbreviation is required");

            if (!City.NotNullOrEmpty())
                AddError(nameof(City), Errors.CityRequired, "City is required");

            if (!StreetName.NotNullOrEmpty())
                AddError(nameof(StreetName), Errors.StreetNameRequired, "Street name is required");

            if (!StateAbbreviation.HasLength(2))
                AddError(nameof(StateAbbreviation), Errors.StateAbbreviationInvalidLength, "State Abbreviation length must be 2");

            if (!City.HasMinLength(3))
                AddError(nameof(City), Errors.CityInvalidLength, "City length must be at least 3");

            if (!StreetName.HasMinLength(3))
                AddError(nameof(StreetName), Errors.StreetNameInvalidLength, "Street name length must be at least 3");
        }

        /// <summary>
        /// Error codes
        /// </summary>
        public static class Errors
        {
            /// <summary>
            /// Error code when state abbreviation is required
            /// </summary>
            public static readonly string StateAbbreviationRequired = "STATE_ABBREVIATION_REQUIRED";
            
            /// <summary>
            /// Error code when city is required
            /// </summary>
            public static readonly string CityRequired= "CITY_REQUIRED";
            
            /// <summary>
            /// Error code when street name is required
            /// </summary>
            public static readonly string StreetNameRequired = "STREET_NAME_REQUIRED";
            
            /// <summary>
            /// Error code when state abbreviation has invalid length
            /// </summary>
            public static readonly string StateAbbreviationInvalidLength = "STATE_ABBREVIATION_INVALID_LENGTH";
            
            /// <summary>
            /// Error code when city has invalid length
            /// </summary>
            public static readonly string CityInvalidLength = "CITY_INVALID_LENGTH";
            
            /// <summary>
            /// Error code when street name has invalid length
            /// </summary>
            public static readonly string StreetNameInvalidLength = "STREET_NAME_INVALID_LENGTH";
        }
    }
}
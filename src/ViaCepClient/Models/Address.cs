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

        }
    }
}
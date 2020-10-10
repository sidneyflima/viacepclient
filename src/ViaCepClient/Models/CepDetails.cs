namespace ViaCepClient.Models
{
    /// <summary>
    /// CepDetails represents cep information retrieved from ViaCep WebService
    /// </summary>
    public class CepDetails
    {
        /// <summary> Cep </summary>
        public string Cep { get; }

        /// <summary> Address </summary>
        public string Address { get; }

        /// <summary> Address complement </summary>
        public string Complement { get; }

        /// <summary> Neighbourhood </summary>
        public string Neighbourhood { get; }

        /// <summary> City </summary>
        public string City { get; }

        /// <summary> Federative Unit </summary>
        public string FederativeUnit { get; }

        /// <summary> IBGE </summary>
        public string IBGE { get; }

        /// <summary> Gia </summary>
        public string GIA  { get; }

        /// <summary> DDD </summary>
        public string DDD { get; }

        /// <summary> Siafi </summary>
        public string Siafi { get; }

        /// <summary>
        /// CepDetails represents cep information retrieved from ViaCep WebService
        /// </summary>
        public CepDetails(string cep, 
                          string address, 
                          string complement,
                          string neighbourhood,
                          string city,
                          string federativeUnit,
                          string ibge,
                          string gia,
                          string ddd,
                          string siafi)
        {
            Cep             = cep;
            Address         = address;
            Complement      = complement;
            Neighbourhood   = neighbourhood;
            City            = city;
            FederativeUnit  = federativeUnit;
            IBGE            = ibge;
            GIA             = gia;
            DDD             = ddd;
            Siafi           = siafi;
        }
    }
}
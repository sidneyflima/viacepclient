namespace ViaCepClient.Models
{
    /// <summary>
    /// CepDetails represents cep information retrieved from ViaCep WebService
    /// </summary>
    public class CepDetails
    {
        /// <summary> 
        /// Cep (cep) 
        /// </summary>
        public string Cep { get; }

        /// <summary> 
        /// Address (logradouro) 
        /// </summary>
        public string Address { get; }

        /// <summary> 
        /// Address complement (complemento) 
        /// </summary>
        public string Complement { get; }

        /// <summary> 
        /// Neighbourhood (bairro) 
        /// </summary>
        public string Neighbourhood { get; }

        /// <summary> 
        /// City (localidade) 
        /// </summary>
        public string City { get; }

        /// <summary> 
        /// Federative Unit (uf) 
        /// </summary>
        public string FederativeUnit { get; }

        /// <summary> 
        /// IBGE (ibge) 
        /// </summary>
        public string IBGE { get; }

        /// <summary> 
        /// Gia (gia) 
        /// </summary>
        public string GIA  { get; }

        /// <summary> 
        /// DDD (ddd) 
        /// </summary>
        public string DDD { get; }

        /// <summary> 
        /// Siafi (siafi) 
        /// </summary>
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
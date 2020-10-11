using ViaCepClient.Models;

namespace ViaCepClient.Converter
{
    /// <summary>
    /// CepDetailsConverter converts a json in a CepDetails object
    /// </summary>
    public class CepDetailsConverter
    {
        /// <summary>
        /// Convert a json in a CepDetails object
        /// </summary>
        public CepDetails FromJson(IPlainJsonObject json)
        {
            return new CepDetails
            (
                cep             : json["cep"], 
                address         : json["logradouro"], 
                complement      : json["complemento"], 
                neighbourhood   : json["bairro"], 
                city            : json["localidade"], 
                federativeUnit  : json["uf"], 
                ibge            : json["ibge"], 
                gia             : json["gia"], 
                ddd             : json["ddd"], 
                siafi           : json["siafi"]
            );
        }
    }
}
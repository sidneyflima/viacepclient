using System.Threading.Tasks;
using ViaCepClient.Models;

namespace ViaCepClient
{
    /// <summary>
    /// IViaCepClient represents a client for ViaCep webservice
    /// </summary>
    public interface IViaCepClient
    {
        /// <summary>
        /// Get cep details information from cep
        /// </summary>
        Task<ResponseMessage<CepDetails>> SendRequest(Cep cep);
    }
}
using System.Threading.Tasks;
using ViaCepClient.Models;

namespace ViaCepClient
{
    /// <summary>
    /// ViaCepClient represents a client for ViaCep webservice
    /// </summary>
    public class ViaCepClient : IViaCepClient
    {
        /// <summary>
        /// Get cep details information from cep
        /// </summary>
        public Task<ResponseMessage<CepDetails>> SendRequest(Cep cep)
        {
            return Task.FromResult(new ResponseMessage<CepDetails>("INVALID_REQUEST"));
        }
    }
}

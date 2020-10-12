using System;
using System.Threading.Tasks;

namespace ViaCepClient.Http
{
    /// <summary>
    /// IRestClient is responsible for http rest methods
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// GetAsync is responsible for send a rest request
        /// </summary>
        Task<RestResponse> GetAsync(Uri uri);
    }
}
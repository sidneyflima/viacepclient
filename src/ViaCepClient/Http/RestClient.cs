using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ViaCepClient.Http
{
    /// <summary>
    /// RestClient is responsible for http rest methods
    /// </summary>
    public class RestClient: IRestClient
    {
        /// <summary>
        /// Inner .NET http client
        /// </summary>
        HttpClient _httpClient;

        /// <summary>
        /// RestClient is responsible for http rest methods
        /// </summary>
        public RestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// GetAsync is responsible for send a rest request
        /// </summary>
        public async Task<RestResponse> GetAsync(Uri uri)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            return new RestResponse(responseMessage, HttpMethod.Get, uri);
        }
    }
}
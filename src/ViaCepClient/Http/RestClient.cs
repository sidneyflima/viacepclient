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
        public Task<RestResponse> GetAsync(Uri uri)
        {
            return _httpClient
                    .GetAsync(uri, HttpCompletionOption.ResponseHeadersRead)
                    .ContinueWith(previousTask => 
                    {
                        HttpResponseMessage responseMessage = previousTask.GetAwaiter().GetResult();
                        return new RestResponse(responseMessage, HttpMethod.Get, uri);
                    });
        }
    }
}
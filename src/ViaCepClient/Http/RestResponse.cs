using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ViaCepClient.Http
{
    /// <summary>
    /// RestResponse represents a result for IRestClient methods
    /// </summary>
    public class RestResponse
    {
        /// <summary>
        /// Inner http response message
        /// </summary>
        HttpResponseMessage _responseMessage;

        /// <summary>
        /// Requested Http Method
        /// </summary>
        public HttpMethod Method { get; }

        /// <summary>
        /// Requested Uri
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        /// Response Status Code
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Check if is a successful response
        /// </summary>
        /// <value></value>
        public bool IsSuccessfulResponse { get; }

        /// <summary>
        /// RestResponse represents a result for IRestClient methods
        /// </summary>
        public RestResponse(HttpResponseMessage responseMessage, HttpMethod method, Uri requestedUri)
        {
            _responseMessage     = responseMessage;
            IsSuccessfulResponse = responseMessage.IsSuccessStatusCode;

            Method      = method;
            Uri         = requestedUri;
            StatusCode  = responseMessage.StatusCode;
        }

        /// <summary>
        /// Get response content as a stream
        /// </summary>
        public Task<Stream> GetResponseStream()
        {
            return _responseMessage.Content.ReadAsStreamAsync();
        }
    }
}
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        /// Response Headers
        /// </summary>
        public ResponseHeaders Headers { get; }

        /// <summary>
        /// Response Content Headers
        /// </summary>
        public ResponseHeaders ContentHeaders { get; }

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
            Headers         = new ResponseHeaders(responseMessage.Headers);
            ContentHeaders  = new ResponseHeaders(responseMessage.Content.Headers);
            Method          = method;
            Uri             = requestedUri;
            StatusCode      = responseMessage.StatusCode;

            IsSuccessfulResponse = responseMessage.IsSuccessStatusCode;
            _responseMessage     = responseMessage;
        }

        /// <summary>
        /// Get response content as a stream
        /// </summary>
        public Task<Stream> GetResponseStream()
        {
            return _responseMessage.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Wrapper for response headers
        /// </summary>
        public class ResponseHeaders
        {
            /// <summary>
            /// Http Response Message
            /// </summary>
            IDictionary<string, IEnumerable<string>> _headerValues;

            /// <summary>
            /// Wrapper for response headers
            /// </summary>
            public ResponseHeaders(HttpHeaders responseHeaders)
            {
                _headerValues = responseHeaders.ToDictionary(e => e.Key, e => e.Value);
            }

            /// <summary>
            /// Get header values from header name
            /// </summary>
            public IEnumerable<string> this[string headerName]
            {
                get
                {
                    if (!_headerValues.TryGetValue(headerName, out IEnumerable<string> headerValues))
                        headerValues = Enumerable.Empty<string>();

                    return headerValues;
                }
            }

            /// <summary>
            /// Get all response header names
            /// </summary>
            public IEnumerable<string> Headers
            {
                get => _headerValues.Keys;
            }
        }
    }
}
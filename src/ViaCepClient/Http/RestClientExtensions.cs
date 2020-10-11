using System.Text.Json;
using System.Threading.Tasks;

namespace ViaCepClient.Http
{
    /// <summary>
    /// RestClientExtensions contains extensions methods for RestClient
    /// </summary>
    public static class RestClientExtensions
    {
        /// <summary>
        /// Get JsonDocument instance from response
        /// </summary>
        public static Task<JsonDocument> AsJsonDocumentAsync(this RestResponse response)
        {
            return response.GetResponseStream()
                           .ContinueWith(t => JsonDocument.ParseAsync(utf8Json: t.GetAwaiter().GetResult()))
                           .Unwrap();
        }
    }
}

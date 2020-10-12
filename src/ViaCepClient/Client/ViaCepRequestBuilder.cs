using System;
using ViaCepClient.Models;

namespace ViaCepClient.Client
{
    /// <summary>
    /// ViaCepRequestBuilder is responsible for
    /// creating request information such as
    /// request Uri
    /// </summary>
    public class ViaCepRequestBuilder : IViaCepRequestBuilder
    {
        /// <summary>
        /// Via cep client options
        /// </summary>
        private readonly ViaCepClientOptions _options;

        /// <summary>
        /// ViaCepRequestBuilder is responsible for
        /// creating request information such as
        /// request Uri
        /// </summary>
        public ViaCepRequestBuilder(ViaCepClientOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Create ViaCep request uri from cep
        /// </summary>
        public Uri CreateRequestUri(Cep cep)
        {
            return new Uri($"{_options.BaseAddress}/ws/{cep.Value}/json/unicode");
        }
    }
}

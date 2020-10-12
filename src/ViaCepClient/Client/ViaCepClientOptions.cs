using System;
using System.Collections.Generic;
using System.Text;

namespace ViaCepClient.Client
{
    /// <summary>
    /// ViaCepClientOptions represents IViaCepClient configuration options
    /// </summary>
    public class ViaCepClientOptions
    {
        /// <summary>
        /// DefaultBaseAddress represents default ViaCep webservice base url address
        /// </summary>
        public const string DefaultBaseAddress = "https://viacep.com.br";

        /// <summary>
        /// Base address private field
        /// </summary>
        private string _baseAddress;

        /// <summary>
        /// BaseAddress retrieve ViaCep webservice base url address,
        /// which is 'DefaultBaseAddress' if nothing has been set
        /// </summary>
        public string BaseAddress => _baseAddress;

        /// <summary>
        /// ViaCepClientOptions represents IViaCepClient configuration options
        /// </summary>
        public ViaCepClientOptions()
        {
            _baseAddress = DefaultBaseAddress;
        }

        /// <summary>
        /// SetBaseAddress gets BaseAddress from parameter uri 
        /// </summary>
        public void SetBaseAddress(Uri baseAddress)
        {
            _baseAddress = baseAddress.Scheme + Uri.SchemeDelimiter + baseAddress.Host;
        }

        /// <summary>
        /// SetBaseAddress gets BaseAddress from parameter string
        /// </summary>
        public void SetBaseAddress(string baseAddress)
        {
            if (Uri.TryCreate(baseAddress, UriKind.RelativeOrAbsolute, out Uri baseAddressUri))
                SetBaseAddress(baseAddressUri);
        }
    }
}

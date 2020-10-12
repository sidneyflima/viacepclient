using System;
using System.Collections.Generic;
using System.Text;
using ViaCepClient.Models;

namespace ViaCepClient.Client
{
    /// <summary>
    /// IViaCepRequestBuilder is responsible for
    /// creating request information such as
    /// request Uri
    /// </summary>
    public interface IViaCepRequestBuilder
    {
        /// <summary>
        /// Create ViaCep request uri from cep
        /// </summary>
        Uri CreateRequestUri(Cep cep);
    }
}

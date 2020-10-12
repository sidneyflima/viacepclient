using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ViaCepClient.Converter;
using ViaCepClient.Http;
using ViaCepClient.Messages;
using ViaCepClient.Models;

namespace ViaCepClient.Client
{
    /// <summary>
    /// ViaCepClient represents a client for ViaCep webservice
    /// </summary>
    public class ViaCepClient : IViaCepClient
    {
        /// <summary>
        /// RestClient
        /// </summary>
        private IRestClient _restClient;

        /// <summary>
        /// Request URL Builder
        /// </summary>
        IViaCepRequestBuilder _requestBuilder;

        /// <summary>
        /// Cep Converter
        /// </summary>
        CepDetailsConverter _cepConverter;

        /// <summary>
        /// ViaCepClient represents a client for ViaCep webservice
        /// </summary>
        public ViaCepClient(IRestClient restClient, IViaCepRequestBuilder requestBuilder)
        {
            _restClient     = restClient;
            _requestBuilder = requestBuilder;
            _cepConverter   = new CepDetailsConverter();
        }

        /// <summary>
        /// Get cep details information from cep
        /// </summary>
        public Task<ResponseMessage<CepDetails>> SendRequestAsync(Cep cep)
        {
            if (cep == null)
                return Task.FromResult(new ResponseMessage<CepDetails>(ErrorCodes.CepRequired, "Cep is required"));
            
            cep.Validate();
            if (cep.IsInvalid())
                return Task.FromResult(new ResponseMessage<CepDetails>(cep.GetValidationErrors().First()));
            
            return SendRequestAndParseResultAsync(cep);
        }

        /// <summary>
        /// Send request to server and parse result
        /// </summary>
        /// <param name="cep"></param>
        /// <returns></returns>
        private async Task<ResponseMessage<CepDetails>> SendRequestAndParseResultAsync(Cep cep)
        {
            RestResponse restResponse = await SendRequestToServerAsync(cep);

            if (!restResponse.IsSuccessfulResponse)
                return CreateResponseErrorWhenRequestNotSuccessful(restResponse);

            return await ParseRestResultAsync(restResponse);
        }

        /// <summary>
        /// Send request to server
        /// </summary>
        private Task<RestResponse> SendRequestToServerAsync(Cep cep)
        {
            Uri requestedUri = _requestBuilder.CreateRequestUri(cep);
            return _restClient.GetAsync(requestedUri);
        }

        /// <summary>
        /// Parse rest response result when success
        /// </summary>
        private async Task<ResponseMessage<CepDetails>> ParseRestResultAsync(RestResponse restResponse)
        {
            using JsonDocument jsonDocument  = await restResponse.AsJsonDocumentAsync();
            IPlainJsonObject plainJsonObject = new PlainJsonObject(jsonDocument);

            if (ResourceHasNotBeenFound(plainJsonObject))
                return new ResponseMessage<CepDetails>(ErrorCodes.ResourceNotFound, "Resource has not been found");

            return new ResponseMessage<CepDetails>(_cepConverter.FromJson(plainJsonObject));
        }

        /// <summary>
        /// Create error response (BadRequest or InternalServerError) when
        /// request is not successful
        /// </summary>
        /// <returns></returns>
        private ResponseMessage<CepDetails> CreateResponseErrorWhenRequestNotSuccessful(RestResponse restResponse)
        {            
            if (restResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return new ResponseMessage<CepDetails>(ErrorCodes.BadRequest, "Bad request");

            return new ResponseMessage<CepDetails>(ErrorCodes.InternalServerError, "Internal server error");   
        }

        /// <summary>
        /// Check if json response has error
        /// </summary>
        private bool ResourceHasNotBeenFound(IPlainJsonObject plainJsonObject)
        {
            return plainJsonObject["erro"] == "true";
        }
    }
}

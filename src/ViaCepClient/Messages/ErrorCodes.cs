using System;
using System.Collections.Generic;
using System.Text;

namespace ViaCepClient.Messages
{
    /// <summary>
    /// ErrorCodes contains error codes strings
    /// </summary>
    public static class ErrorCodes
    {
        #region Address

        /// <summary>
        /// Error code when state abbreviation is required
        /// </summary>
        public const string StateAbbreviationRequired = "STATE_ABBREVIATION_REQUIRED";

        /// <summary>
        /// Error code when city is required
        /// </summary>
        public const string CityRequired = "CITY_REQUIRED";

        /// <summary>
        /// Error code when street name is required
        /// </summary>
        public const string StreetNameRequired = "STREET_NAME_REQUIRED";

        /// <summary>
        /// Error code when state abbreviation has invalid length
        /// </summary>
        public const string StateAbbreviationInvalidLength = "STATE_ABBREVIATION_INVALID_LENGTH";

        /// <summary>
        /// Error code when city has invalid length
        /// </summary>
        public const string CityInvalidLength = "CITY_INVALID_LENGTH";

        /// <summary>
        /// Error code when street name has invalid length
        /// </summary>
        public const string StreetNameInvalidLength = "STREET_NAME_INVALID_LENGTH";

        #endregion

        #region Cep

        /// <summary>
        /// Error code when cep is required
        /// </summary>
        public const string CepRequired = "CEP_REQUIRED";

        /// <summary>
        /// Error code when cep has invalid pattern
        /// </summary>
        public const string CepInvalidPattern = "CEP_INVALID_PATTERN";

        #endregion

        #region Request

        /// <summary>
        /// Error code when request return BadRequest
        /// </summary>
        public const string BadRequest = "BAD_REQUEST";

        /// <summary>
        /// Error code when request return InternalServerError
        /// </summary>
        public const string InternalServerError = "INTERNAL_SERVER_ERROR";

        /// <summary>
        /// Error code when requested data has been not found
        /// </summary>
        public const string ResourceNotFound = "RESOURCE_NOT_FOUND";

        #endregion
    }
}

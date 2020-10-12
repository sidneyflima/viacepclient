using ViaCepClient.Validators;

namespace ViaCepClient.Models
{
    /// <summary>
    /// ResponseMessage represents a response contract which contains
    /// response body content and other informations, such as errors
    /// </summary>
    public class ResponseMessage<T> where T : class
    {
        /// <summary>
        /// If request has been processed and returned
        /// a successful response
        /// </summary>
        public bool IsSuccessfulResponse { get; }

        /// <summary>
        /// Response Error Message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Request property name which contains error
        /// </summary>
        public string ErrorPropertyName { get; set; }

        /// <summary>
        /// Response Error Code
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Deserialized response content 
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// ResponseMessage represents a response contract which contains
        /// response body content and other informations, such as errors
        /// </summary>
        public ResponseMessage(T content)
        {
            IsSuccessfulResponse = true;
            ErrorCode            = null;
            ErrorMessage         = null;
            ErrorPropertyName    = null;
            Content              = content;
        }

        /// <summary>
        /// ResponseMessage represents a response contract which contains
        /// response body content and other informations, such as errors
        /// </summary>
        public ResponseMessage(string errorCode, string errorMessage)
        {
            IsSuccessfulResponse = false;
            ErrorCode            = errorCode;
            ErrorMessage         = errorMessage;
            ErrorPropertyName    = null;
            Content              = null;
        }

        /// <summary>
        /// ResponseMessage represents a response contract which contains
        /// response body content and other informations, such as errors
        /// </summary>
        public ResponseMessage(IError error)
        {
            IsSuccessfulResponse = false;
            ErrorCode            = error.ErrorCode;
            ErrorMessage         = error.ErrorMessage;
            ErrorPropertyName    = error.PropertyName;
            Content              = null;
        }
    }
}

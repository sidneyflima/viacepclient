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
            ErrorMessage         = string.Empty;
            Content              = content;
        }

        /// <summary>
        /// ResponseMessage represents a response contract which contains
        /// response body content and other informations, such as errors
        /// </summary>
        public ResponseMessage(string errorMessage)
        {
            IsSuccessfulResponse = false;
            ErrorMessage         = errorMessage;
            Content              = null;
        }
    }
}

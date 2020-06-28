namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// Error represents a validation error
    /// </summary>
    internal class Error: IError
    {
        /// <summary>
        /// Message represents the message error
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Error represents a validation error
        /// </summary>
        public Error(string code, string message)
        {
            Code    = code;
            Message = message;
        }
    }
}
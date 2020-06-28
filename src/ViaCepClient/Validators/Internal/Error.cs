namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// Error represents a validation error
    /// </summary>
    internal struct Error: IError
    {
        /// <summary>
        /// Code represents a unique code representation for error
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// PropertyName represents the model property name which has an error
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Message represents the message error
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Error represents a validation error
        /// </summary>
        public Error(string errorCode, string propertyName, string message)
        {
            ErrorCode       = errorCode;
            PropertyName    = propertyName;
            Message         = message;
        }
    }
}
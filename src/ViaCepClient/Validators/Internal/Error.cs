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
        /// Error Message
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Error represents a validation error
        /// </summary>
        public Error(string propertyName, string errorCode, string errorMessage)
        {
            ErrorCode       = errorCode;
            ErrorMessage    = errorMessage;
            PropertyName    = propertyName;
        }
    }
}
namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// RuleResult represents a result for applied rule specification
    /// </summary>
    internal struct RuleResult: IRuleResult
    {
        /// <summary>
        /// Check if rule is valid
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Error code
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// RuleResult represents a result for applied rule specification
        /// </summary>
        public RuleResult(bool isValid, string errorCode, string errorMessage)
        {
            IsValid      = isValid;
            ErrorCode    = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}

namespace ViaCepClient.Validators
{
    /// <summary>
    /// IRuleResult represents a result for applied rule specification
    /// </summary>
    public interface IRuleResult
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
        /// Get error message
        /// </summary>
        public string ErrorMessage { get; }
    }
}

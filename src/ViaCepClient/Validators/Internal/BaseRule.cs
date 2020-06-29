namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// BaseRule represents an abstract struture for rule specifications
    /// </summary>
    internal abstract class BaseRule<TValue> : IRule<TValue>
    {
        /// <summary>
        /// Error code assigned to rule specification
        /// </summary>
        private readonly string _errorCode;

        /// <summary>
        /// Error message 
        /// </summary>
        private readonly string _errorMessage;

        /// <summary>
        /// Get Default Error Code
        /// </summary>
        protected abstract string DefaultErrorCode { get; }

        /// <summary>
        /// Get Default Error Message
        /// </summary>
        protected abstract string DefaultErrorMessage { get; }

        /// <summary>
        /// BaseRule represents an abstract struture for rule specifications
        /// </summary>
        public BaseRule(string errorCode, string errorMessage)
        {
            _errorCode    = !string.IsNullOrEmpty(errorCode)     ? errorCode     : DefaultErrorCode;
            _errorMessage = !string.IsNullOrEmpty(errorMessage)  ? errorMessage  : DefaultErrorMessage;
        }

        /// <summary>
        /// Apply rule specification
        /// </summary>
        public IRuleResult ApplyRule(TValue value)
        {
            if (IsRuleValid(value))
                return new RuleResult(true, null, null);

            return new RuleResult(false, _errorCode, _errorMessage);
        }

        /// <summary>
        /// Check if rule is valid for value
        /// </summary>
        protected abstract bool IsRuleValid(TValue value);
    }
}

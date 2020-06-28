namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// Represents a rule result
    /// </summary>
    internal struct RuleResult: IRuleResult
    {
        /// <summary>
        /// IsValid returns true if value is valid for rule specification
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Error returns an error message if value is invalid
        /// </summary>
        public IError Error { get; }

        /// <summary>
        /// Represents a rule result
        /// </summary>
        public RuleResult(bool isValid, IError error)
        {
            IsValid = isValid;
            Error   = error;
        }

        /// <summary>
        /// Creates a new valid RuleResult
        /// </summary>
        public static RuleResult ValidRuleResult()
        {
            return new RuleResult(true, null);
        }

        /// <summary>
        /// Creates a new invalid RuleResult
        /// </summary>
        public static RuleResult InvalidRuleResult(IError error)
        {
            return new RuleResult(false, error);
        }
    }
}
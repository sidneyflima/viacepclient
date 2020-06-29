namespace ViaCepClient.Validators.Internal.Rules
{
    /// <summary>
    /// Rule for string not null or empty
    /// </summary>
    internal class StringNotEmptyRule : BaseRule<string>
    {
        /// <summary>
        /// Get Default Error Code
        /// </summary>
        protected override string DefaultErrorCode => "STRING_NOT_EMPTY";

        /// <summary>
        /// Get Default Error Message
        /// </summary>
        protected override string DefaultErrorMessage => "String is null or empty";

        /// <summary>
        /// Rule for string not null or empty
        /// </summary>
        public StringNotEmptyRule(string errorCode, string errorMessage) 
        : base(errorCode, errorMessage)
        {
        }

        /// <summary>
        /// Check if rule is valid
        /// </summary>
        protected override bool IsRuleValid(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}

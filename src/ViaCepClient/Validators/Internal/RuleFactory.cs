using ViaCepClient.Validators.Internal.Rules;

namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// Rule specifications
    /// </summary>
    internal static class RuleFactory
    {
        /// <summary>
        /// Create a rule for string not null or empty
        /// </summary>
        public static IRule<string> StringNotEmpty(string errorCode = null, string errorMessage = null)
        {
            return new StringNotEmptyRule(errorCode, errorMessage);
        }
    }
}

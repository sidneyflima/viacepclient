using System;
using System.Collections.Generic;
using System.Text;
using ViaCepClient.Validators.Internal.Rules;

namespace ViaCepClient.Validators
{
    /// <summary>
    /// Rule specifications
    /// </summary>
    public static class RuleSpecifications
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

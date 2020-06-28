using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

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
        public string ErrorCode { get; }

        /// <summary>
        /// BaseRule represents an abstract struture for rule specifications
        /// </summary>
        public BaseRule(string errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Apply rule specification
        /// </summary>
        public IRuleResult ApplyRule(TValue value)
        {
            if (IsRuleValid(value))
                return new RuleResult(true, null);

            string errorMessage = GetFormattedErrorMessage(value);
            return new RuleResult(false, errorMessage);
        }

        /// <summary>
        /// Check if rule is valid for value
        /// </summary>
        protected abstract bool IsRuleValid(TValue value);

        /// <summary>
        /// Check formatted error message if rule is not valid
        /// </summary>
        protected abstract string GetFormattedErrorMessage(TValue value);
    }
}

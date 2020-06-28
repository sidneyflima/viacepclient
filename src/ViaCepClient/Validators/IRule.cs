using System;
using System.Collections.Generic;
using System.Text;

namespace ViaCepClient.Validators
{
    /// <summary>
    /// IRule represents a rule specification used to validate a property value
    /// </summary>
    public interface IRule<TValue>
    {
        /// <summary>
        /// Error code assigned to rule specification
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Apply rule specification
        /// </summary>
        IRuleResult ApplyRule(TValue value);
    }
}

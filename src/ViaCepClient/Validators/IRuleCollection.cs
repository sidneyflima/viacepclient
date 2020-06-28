using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ViaCepClient.Validators
{
    /// <summary>
    /// RuleCollection represents a collection of rules for TModel model
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    public interface IRuleCollection<TModel>
    {
        /// <summary>
        /// Add validation rule for a model property
        /// </summary>
        /// <typeparam name="TPropertyValue">type of model property</typeparam>
        /// <param name="expression">expression used to inform a model property</param>
        /// <param name="rule">rule instance</param>
        /// <returns>The same rule collection</returns>
        IRuleCollection<TModel> AddRuleFor<TPropertyValue>(Expression<Func<TModel, TPropertyValue>> expression, IRule<TPropertyValue> rule);
    }
}

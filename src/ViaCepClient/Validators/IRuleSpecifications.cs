using System;
using System.Linq.Expressions;

namespace ViaCepClient.Validators
{
    /// <summary>
    /// IRuleSpecifications represents a collection of rules for TModel model
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    public interface IRuleSpecifications<TModel>
    {
        /// <summary>
        /// Add validation rule for a model property
        /// </summary>
        /// <typeparam name="TPropertyValue">type of model property</typeparam>
        /// <param name="expression">expression used to inform a model property</param>
        /// <param name="rule">rule instance</param>
        /// <returns>The same rule collection</returns>
        IRuleSpecifications<TModel> AddRuleFor<TPropertyValue>(Expression<Func<TModel, TPropertyValue>> expression, IRule<TPropertyValue> rule);
    }
}

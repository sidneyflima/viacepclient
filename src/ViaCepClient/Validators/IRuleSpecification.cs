using System;
using System.Linq.Expressions;

namespace ViaCepClient.Validators
{
    /// <summary>
    /// IRuleSpecification defines model validation rules 
    /// </summary>
    public interface IRuleSpecification<TModel>
    {
        /// <summary>
        /// Set rules for model property
        /// </summary>
        IRuleSpecification<TModel> SetRules<TPropertyValue>(Expression<Func<TModel, TPropertyValue>> expression, params IRule<TPropertyValue>[] rules);
    }
}

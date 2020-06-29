using System.Collections.Generic;

namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// IRuleSpecificationProperty represents rule 
    /// specification model property information, wrapping
    /// rule validation for model property
    /// </summary>
    internal interface IRuleSpecificationProperty<TModel>
    {
        /// <summary>
        /// Property name
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// Wrap rule specification functionalities and
        /// apply rule for a property
        /// </summary>
        IEnumerable<IRuleResult> ApplyRules(TModel model);
    }

    /// <summary>
    /// IRuleSpecificationProperty represents rule 
    /// specification model property information, wrapping
    /// rule validation for model property
    /// </summary>
    internal interface IRuleSpecificationProperty<TModel, TPropertyValue> : IRuleSpecificationProperty<TModel>
    {
        /// <summary>
        /// Add rule to collection
        /// </summary>
        void AddRule(IRule<TPropertyValue> rule);
    }
}

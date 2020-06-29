using System;
using System.Collections.Generic;

namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// IRuleSpecificationProperty represents rule 
    /// specification model property information, wrapping
    /// rule validation for model property
    /// </summary>
    internal class RuleSpecificationProperty<TModel, TPropertyValue>: IRuleSpecificationProperty<TModel, TPropertyValue>
    {
        /// <summary>
        /// GetValue function, retrieve property value from model instance
        /// </summary>
        private readonly Func<TModel, TPropertyValue> _getValue;

        /// <summary>
        /// Rule Collection
        /// </summary>
        private readonly List<IRule<TPropertyValue>> _rules;

        /// <summary>
        /// IRuleSpecificationProperty represents rule 
        /// specification model property information, wrapping
        /// rule validation for model property
        /// </summary>
        public RuleSpecificationProperty(string propertyName, Func<TModel, TPropertyValue> getValueFunc)
        {
            _getValue = getValueFunc;
            _rules    = new List<IRule<TPropertyValue>>();

            PropertyName = propertyName;
        }

        /// <summary>
        /// Property name
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Wrap rule specification functionalities and
        /// apply rule for a property
        /// </summary>
        public IEnumerable<IRuleResult> ApplyRules(TModel model)
        {
            List<IRuleResult> result = new List<IRuleResult>(_rules.Count);

            TPropertyValue value = GetPropertyValue(model);

            foreach(IRule<TPropertyValue> rule in _rules)
                result.Add(rule.ApplyRule(value));
 
            return result;
        }

        /// <summary>
        /// Add rule to collection
        /// </summary>
        public void AddRule(IRule<TPropertyValue> rule)
        {
            if (rule != null)
            {
                _rules.Add(rule);
            }
        }

        /// <summary>
        /// Get property value
        /// </summary>
        private TPropertyValue GetPropertyValue(TModel model)
        {
            return _getValue(model);
        }
    }
}

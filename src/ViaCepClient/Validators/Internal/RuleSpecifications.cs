using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ViaCepClient.Extensions;

namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// RuleCollection represents a collection of rules for TModel model
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    internal class RuleSpecifications<TModel>: IRuleSpecifications<TModel>
    {
        /// <summary>
        /// Rule specification property map
        /// </summary>
        IDictionary<string, IRuleSpecificationProperty<TModel>> _propertyMap;

        /// <summary>
        /// Rules count
        /// </summary>
        int _ruleSpecificationsCount = 0;

        /// <summary>
        /// Rule specification count
        /// </summary>
        public int RuleSpecificationsCount => _ruleSpecificationsCount;

        /// <summary>
        /// Add validation rule for a model property
        /// </summary>
        /// <typeparam name="TPropertyValue">type of model property</typeparam>
        /// <param name="expression">expression used to inform a model property</param>
        /// <param name="rule">rule instance</param>
        /// <returns>The same rule collection</returns>
        public IRuleSpecifications<TModel> AddRuleFor<TPropertyValue>(Expression<Func<TModel, TPropertyValue>> expression, IRule<TPropertyValue> rule)
        {
            if (rule == null)
                return this;

            var propertyName = expression.GetPropertyName();
            if (string.IsNullOrEmpty(propertyName))
                return this;

            var ruleSpecificationProperty = GetRuleSpecificationFromMap<TPropertyValue>(propertyName);
            if (ruleSpecificationProperty == null)
            {
                var getValueFunction      = expression.ExtractFunction();
                ruleSpecificationProperty = new RuleSpecificationProperty<TModel, TPropertyValue>(propertyName, getValueFunction);
            }

            ruleSpecificationProperty.AddRule(rule);
            _rulesCount++;

            return this;
        }

        /// <summary>
        /// Get rule specification properties
        /// </summary>
        public IEnumerable<IRuleSpecificationProperty<TModel>> GetRuleSpecificationProperties()
        {
            return _propertyMap.Values;
        }

        /// <summary>
        /// Get rule specification property from inner property map
        /// </summary>
        /// <typeparam name="TPropertyValue"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private IRuleSpecificationProperty<TModel, TPropertyValue> GetRuleSpecificationFromMap<TPropertyValue>(string propertyName)
        {
            if (_propertyMap.TryGetValue(propertyName, out IRuleSpecificationProperty<TModel> property))
            {
                if(property is IRuleSpecificationProperty<TModel, TPropertyValue> specificationProperty)
                {
                    return specificationProperty;
                }
            }

            return null;
        }
    }
}

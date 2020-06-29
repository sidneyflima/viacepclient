using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ViaCepClient.Extensions;

namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// IRuleSpecification defines model validation rules 
    /// </summary>
    internal class RuleSpecification<TModel>: IRuleSpecification<TModel>
    {
        /// <summary>
        /// Map of IModelPropertyRules
        /// </summary>
        private readonly IDictionary<string, IModelPropertyRules<TModel>> _modelPropertyMap;

        /// <summary>
        /// IRuleSpecification defines model validation rules 
        /// </summary>
        public RuleSpecification()
        {
            _modelPropertyMap = new Dictionary<string, IModelPropertyRules<TModel>>();
        }

        /// <summary>
        /// Set rules for model property
        /// </summary>
        public IRuleSpecification<TModel> SetRules<TPropertyValue>(Expression<Func<TModel, TPropertyValue>> expression, params IRule<TPropertyValue>[] rules)
        {
            if (expression == null)
                return this;

            var propertyName = expression.GetPropertyName();
            if (string.IsNullOrEmpty(propertyName))
                return this;

            if (ModelPropertyExists(propertyName))
                return this;

            var validRules = FilterValidRules(rules);
            if (!validRules.Any())
                return this;

            var getValueFunction   = expression.ExtractFunction();
            var modelPropertyRules = new ModelPropertyRules<TModel, TPropertyValue>(propertyName, getValueFunction, rules);

            _modelPropertyMap.Add(propertyName, modelPropertyRules);

            return this;
        }

        /// <summary>
        /// Get model property rules
        /// </summary>
        public IEnumerable<IModelPropertyRules<TModel>> GetModelPropertyRules()
        {
            return _modelPropertyMap.Values;
        }

        /// <summary>
        /// Check if there are rules defined
        /// </summary>
        private IEnumerable<IRule<TPropertyValue>> FilterValidRules<TPropertyValue>(IRule<TPropertyValue>[] rules)
        {
            if (rules == null || rules.Length <= 0)
                return Enumerable.Empty<IRule<TPropertyValue>>();

            List<IRule<TPropertyValue>> result = new List<IRule<TPropertyValue>>(rules.Length);
            
            foreach(var rule in rules)
            {
                if (rule != null)
                    result.Add(rule);
            }

            return result;
        }

        /// <summary>
        /// Check if model property exists
        /// </summary>
        private bool ModelPropertyExists(string propertyName)
        {
            return _modelPropertyMap.ContainsKey(propertyName);
        }
    }
}

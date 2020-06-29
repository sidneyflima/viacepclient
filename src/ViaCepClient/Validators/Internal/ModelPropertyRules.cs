using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// Rule collection for a model property
    /// </summary>
    internal class ModelPropertyRules<TModel, TPropertyValue>: IModelPropertyRules<TModel>
    {
        /// <summary>
        /// Rule collection
        /// </summary>
        private readonly IEnumerable<IRule<TPropertyValue>> _rules;

        /// <summary>
        /// Function that get value from model property
        /// </summary>
        private readonly Func<TModel, TPropertyValue> _getValue;

        /// <summary>
        /// Rules count
        /// </summary>
        private readonly int _rulesCount = 0;

        /// <summary>
        /// Property name
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Rule collection for a model property
        /// </summary>
        public ModelPropertyRules(string propertyName, Func<TModel, TPropertyValue> getValue, IEnumerable<IRule<TPropertyValue>> rules)
        {
            PropertyName = propertyName;
            _getValue    = getValue;
            _rules       = rules;
            _rulesCount  = rules?.Count() ?? 0;
        }

        /// <summary>
        /// Apply rules
        /// </summary>
        public IEnumerable<IRuleResult> ApplyRules(TModel model)
        {
            if (_rules == null || !_rules.Any())
                return Enumerable.Empty<IRuleResult>();

            List<IRuleResult> result = new List<IRuleResult>(_rulesCount);

            foreach(var rule in _rules)
            {
                var value  = _getValue(model);
                result.Add(rule.ApplyRule(value));
            }

            return result;
        }
    }
}

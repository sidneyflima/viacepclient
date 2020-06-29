using System;
using System.Collections.Generic;
using System.Text;

namespace ViaCepClient.Validators.Internal
{
    /// <summary>
    /// Rule collection for a model property
    /// </summary>
    internal interface IModelPropertyRules<TModel>
    {
        /// <summary>
        /// Property name
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// Apply rules
        /// </summary>
        IEnumerable<IRuleResult> ApplyRules(TModel model);
    }
}

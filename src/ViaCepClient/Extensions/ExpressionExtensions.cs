using System;
using System.Linq.Expressions;

namespace ViaCepClient.Extensions
{
    /// <summary>
    /// Extension methods for expressions
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// If expression for a property, get property name
        /// </summary>
        public static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            if (expression != null && expression.Body is MemberExpression memberExpression)
                return memberExpression.Member.Name;

            return string.Empty;
        }

        /// <summary>
        /// Extract a function for a property
        /// </summary>
        public static Func<T, TProperty> ExtractFunction<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            return expression?.Compile();
        }
    }
}

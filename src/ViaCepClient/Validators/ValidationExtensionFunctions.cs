using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ViaCepClient.Validators
{
    /// <summary>
    /// Extension functions for validations
    /// </summary>
    public static class ValidationExtensionFunctions
    {
        /// <summary>
        /// Check if string is not null or empty
        /// </summary>
        public static bool NotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Check if object is not null
        /// </summary>
        public static bool NotNull<T>(this T obj)
        {
            return obj != null;
        }

        /// <summary>
        /// Check if array has length
        /// </summary>
        public static bool HasLength<T>(this T[] items, int length) 
        {
            return items != null && items.Length == length;
        }

        /// <summary>
        /// Check if string has length 
        /// </summary>
        public static bool HasLength(this string str, int length)
        {
            return str != null && str.Length == length;
        }

        /// <summary>
        /// Check if string has maximum length (or equal)
        /// </summary>
        public static bool HasMaxLength(this string str, int maxLength)
        {
            return str != null && str.Length <= maxLength;
        }

        /// <summary>
        /// Check if string has minimal length (or equal)
        /// </summary>
        public static bool HasMinLength(this string str, int minLength)
        {
            return str != null && str.Length >= minLength;
        }

        /// <summary>
        /// Check if regex matches
        /// </summary>
        public static bool RegexMatches(this string str, Regex regex)
        {
            if (regex == null)
                return false;

            return regex.IsMatch(str);
        }

        /// <summary>
        /// Check if regex matches
        /// </summary>
        public static bool RegexMatches(this string str, string pattern)
        {
            if (pattern == null)
                return false;

            try
            {
                return str.RegexMatches(new Regex(pattern));
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}

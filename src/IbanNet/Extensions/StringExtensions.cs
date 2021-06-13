using System;
using System.Diagnostics.CodeAnalysis;

namespace IbanNet.Extensions
{
    internal static class StringExtensions
    {
        public static string? StripWhitespaceOrNull([NotNullIfNotNull("value")] this string? value)
        {
            return value is null ? null : StripWhitespace(value);
        }

        /// <summary>
        /// Removes all whitespace.
        /// </summary>
        public static string StripWhitespace(this string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            char[] buffer = new char[value.Length];
            int pos = 0;
            // ReSharper disable once ForCanBeConvertedToForeach - justification : performance
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (!c.IsWhitespace())
                {
                    buffer[pos++] = c;
                }
            }

            return new string(buffer, 0, pos);
        }
    }
}

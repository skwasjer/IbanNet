using System;
using System.ComponentModel;
using System.Globalization;
using IbanNet.Extensions;

namespace IbanNet.Registry.Parsing
{
    /// <summary>
    /// Defines a token that spans one or more characters of the same <see cref="AsciiCategory" />.
    /// </summary>
    public sealed class PatternToken
    {
        /// <summary>
        /// Initializes a new instance of the pattern token.
        /// </summary>
        /// <param name="category">The ASCII category for the token.</param>
        /// <param name="length">The length of the token.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="length" /> is less than 1.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when <paramref name="category" /> is an invalid value.</exception>
        public PatternToken(AsciiCategory category, int length)
            : this(category, length, length, nameof(length))
        {
        }

        /// <summary>
        /// Initializes a new instance of the pattern token.
        /// </summary>
        /// <param name="category">The ASCII category for the token.</param>
        /// <param name="minLength">The minimum length of the token.</param>
        /// <param name="maxLength">The maximum length of the token.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="minLength" /> or <paramref name="maxLength" /> is less than 1, or <paramref name="maxLength" /> is less than <paramref name="minLength" />.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when <paramref name="category" /> is an invalid value.</exception>
        public PatternToken(AsciiCategory category, int minLength, int maxLength)
            : this(category, minLength, maxLength, nameof(minLength))
        {
        }

        private PatternToken(AsciiCategory category, int minLength, int maxLength, string minLengthPropertyName)
        {
            if (minLength <= 0)
            {
                throw new ArgumentOutOfRangeException(minLengthPropertyName, string.Format(CultureInfo.CurrentCulture, Resources.The_value_cannot_be_less_than_or_equal_to_0, 0));
            }

            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength), string.Format(CultureInfo.CurrentCulture, Resources.The_value_cannot_be_less_than_or_equal_to_0, 0));
            }

            if (maxLength < minLength)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength), string.Format(CultureInfo.CurrentCulture, Resources.The_value_cannot_be_less_than_or_equal_to_0, minLength));
            }

            if (!Enum.IsDefined(typeof(AsciiCategory), category))
            {
#if NETSTANDARD1_2 || NETSTANDARD1_6
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Enum_value_0_should_be_defined_in_the_1_enum, category, nameof(AsciiCategory)), nameof(category));
#else
                throw new InvalidEnumArgumentException(nameof(category), (int)category, typeof(AsciiCategory));
#endif
            }

            Category = category;
            MinLength = minLength;
            MaxLength = maxLength;
            IsFixedLength = minLength == MaxLength;
            IsMatch = GetCharacterTest(category);
        }

        /// <summary>
        /// Gets the ASCII category for this token.
        /// </summary>
        public AsciiCategory Category { get; }

        /// <summary>
        /// Gets the minimum length of this token.
        /// </summary>
        public int MinLength { get; }

        /// <summary>
        /// Gets the maximum length of this token.
        /// </summary>
        public int MaxLength { get; }

        /// <summary>
        /// Gets whether or not this token is fixed length.
        /// </summary>
        internal bool IsFixedLength { get; }

        internal Func<char, bool> IsMatch { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return IsFixedLength
                ? $"{Category}[{MaxLength}]"
                : $"{Category}[{MinLength}-{MaxLength}]";
        }

        private static Func<char, bool> GetCharacterTest(AsciiCategory category)
        {
            return category switch
            {
                AsciiCategory.Space => ch => ch == ' ',
                AsciiCategory.Digit => CharExtensions.IsAsciiDigit,
                AsciiCategory.AlphaNumeric => CharExtensions.IsAlphaNumeric,
                AsciiCategory.UppercaseLetter => CharExtensions.IsUpperAsciiLetter,
                AsciiCategory.LowercaseLetter => CharExtensions.IsLowerAsciiLetter,
                AsciiCategory.Letter => CharExtensions.IsAsciiLetter,
                _ => _ => false
            };
        }
    }
}

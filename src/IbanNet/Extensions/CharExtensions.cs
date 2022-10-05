using System.Diagnostics;

namespace IbanNet.Extensions
{
    /// <summary>
    /// Taken partially from MS source for <see cref="char" />.
    /// </summary>
    [DebuggerNonUserCode]
    internal static class CharExtensions
    {
        public static bool IsInRange(this char c, char min, char max)
        {
            // ReSharper disable RedundantCast - justification: more clear this way.
            return (uint)c - (uint)min <= (uint)max - (uint)min;
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// Returns true if char is 0-9, a-z or A-Z and false otherwise.
        /// </summary>
        public static bool IsAlphaNumeric(this char ch)
        {
            ch |= ' ';
            return IsInRange(ch, '0', '9') || IsInRange(ch, 'a', 'z');
        }

        public static bool IsAsciiLetter(this char ch)
        {
            ch |= ' ';
            return IsInRange(ch, 'a', 'z');
        }

        public static bool IsLowerAsciiLetter(this char ch)
        {
            return IsInRange(ch, 'a', 'z');
        }

        public static bool IsUpperAsciiLetter(this char ch)
        {
            return IsInRange(ch, 'A', 'Z');
        }

        public static bool IsAsciiDigit(this char ch)
        {
            return IsInRange(ch, '0', '9');
        }

        public static bool IsSingleLineWhitespace(this char ch)
        {
            return ch is ' ' or '\t';
        }
    }
}

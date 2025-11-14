using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace IbanNet.Extensions;

/// <summary>
/// Taken partially from MS source for <see cref="char" />. Polyfills some newer API's for the older TFM's.
/// </summary>
[DebuggerNonUserCode]
internal static class CharExtensions
{
    extension(char)
    {
#if !NET8_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsInRange(char c, char min, char max)
        {
            // ReSharper disable RedundantCast - justification: more clear this way.
            return (uint)c - (uint)min <= (uint)max - (uint)min;
            // ReSharper restore RedundantCast
        }

        /// <summary>
        /// Returns true if char is 0-9, a-z or A-Z and false otherwise.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAsciiLetterOrDigit(char c)
        {
            c |= ' ';
            return IsInRange(c, '0', '9') || IsInRange(c, 'a', 'z');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAsciiLetter(char c)
        {
            c |= ' ';
            return IsInRange(c, 'a', 'z');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAsciiLetterLower(char c)
        {
            return IsInRange(c, 'a', 'z');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAsciiLetterUpper(char c)
        {
            return IsInRange(c, 'A', 'Z');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAsciiDigit(char c)
        {
            return IsInRange(c, '0', '9');
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSingleLineWhitespace(char c)
        {
            return c is ' ' or '\t';
        }
    }
}

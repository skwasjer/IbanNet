using System.Diagnostics.CodeAnalysis;
using IbanNet.Extensions;

namespace IbanNet.Internal;

internal static class InputNormalization
{
    /// <summary>
    /// Normalizes an IBAN by removing whitespace, removing non-alphanumerics and upper casing each character.
    /// </summary>
    /// <param name="value">The input value to normalize.</param>
    /// <returns>The normalized IBAN.</returns>
    internal static string? NormalizeOrNull([NotNullIfNotNull("value")] string? value)
    {
        if (value is null)
        {
            return null;
        }

        int length = value.Length;
#if USE_SPANS
        // Use stack but clamp to avoid excessive stackalloc buffer.
        const int stackallocMaxSize = Iban.MaxLength + 6;
        Span<char> buffer = length <= stackallocMaxSize
            ? stackalloc char[length]
            : new char[length];
#else
            char[] buffer = new char[length];
#endif
        int pos = 0;
        // ReSharper disable once ForCanBeConvertedToForeach - justification : performance
        for (int i = 0; i < length; i++)
        {
            char ch = value[i];
            if (ch.IsWhitespace())
            {
                continue;
            }

            if (ch.IsAsciiLetter())
            {
                // Inline upper case.
                buffer[pos++] = (char)(ch & ~' ');
            }
            else
            {
                buffer[pos++] = ch;
            }
        }

#if USE_SPANS
        return new string(buffer[..pos]);
#else
            return new string(buffer, 0, pos);
#endif
    }
}

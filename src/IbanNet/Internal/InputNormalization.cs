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
#if USE_SPANS
    internal static string? NormalizeOrNull([NotNullIfNotNull("value")] string? value)
    {
        return value is null ? null : Normalize(value.AsSpan()).ToString();
    }

    internal static ReadOnlySpan<char> Normalize(ReadOnlySpan<char> value)
    {
#else
    internal static string? NormalizeOrNull([NotNullIfNotNull("value")] string? value)
    {
        if (value is null)
        {
            return null;
        }
#endif

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
        bool hasModified = false;
        for (int i = 0; i < length; i++)
        {
            char ch = value[i];
            if (ch.IsSingleLineWhitespace())
            {
                hasModified = true;
                continue;
            }

            if (ch.IsAsciiLetter())
            {
                // Inline upper case.
                char newCh = (char)(ch & ~' ');
                hasModified |= ch != newCh;
                buffer[pos++] = newCh;
            }
            else
            {
                buffer[pos++] = ch;
            }
        }

#if USE_SPANS
        return hasModified
            ? (ReadOnlySpan<char>)buffer[..pos].ToArray()
            : value; // Unmodified
#else
        return hasModified
            ? new string(buffer, 0, pos)
            : value; // Unmodified
#endif
    }
}

using System.Globalization;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Swift;

/// <remarks>
/// https://www.swift.com/standards/data-standards/iban
/// length
/// ! = fixed
/// marker
/// </remarks>
internal class SwiftPatternTokenizer : PatternTokenizer
{
    private static readonly char[] TokenChars = { 'n', 'a', 'c', 'e' };

    internal SwiftPatternTokenizer() : base(TokenChars.Contains)
    {
    }

    protected override AsciiCategory GetCategory(string token)
    {
        if (token.Length < 2)
        {
            return AsciiCategory.None;
        }

        // ReSharper disable once UseIndexFromEndExpression
        char tokenChar = token[token.Length - 1];
        return tokenChar switch
        {
            'n' => AsciiCategory.Digit,
            'a' => AsciiCategory.UppercaseLetter,
            'c' => AsciiCategory.AlphaNumeric,
            'e' => AsciiCategory.Space,
            _ => AsciiCategory.None
        };
    }

    protected override int GetLength(string token, out bool isFixedLength)
    {
        if (token.Length < 2)
        {
            isFixedLength = true;
            return -1;
        }

#if USE_SPANS
        ReadOnlySpan<char> lengthDescriptor = token.AsSpan(0, token.Length - 1);
        // ReSharper disable once UseIndexFromEndExpression
        isFixedLength = lengthDescriptor[^1] == '!';
        return int.Parse(
            lengthDescriptor[..^Convert.ToByte(isFixedLength)],
            NumberStyles.None,
            CultureInfo.InvariantCulture
        );
#else
            string lengthDescriptor = token.Substring(0, token.Length - 1);
            // ReSharper disable once UseIndexFromEndExpression
            isFixedLength = lengthDescriptor[lengthDescriptor.Length - 1] == '!';
            return int.Parse(
                lengthDescriptor.Substring(0, lengthDescriptor.Length - Convert.ToByte(isFixedLength)),
                NumberStyles.None,
                CultureInfo.InvariantCulture
            );
#endif
    }
}

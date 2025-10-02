using System.Globalization;
using IbanNet.Extensions;
using IbanNet.Registry.Patterns;

namespace IbanNet.CodeGen.Swift;

/// <remarks>
/// https://www.swift.com/standards/data-standards/iban
/// length
/// ! = fixed
/// marker
/// </remarks>
internal class SwiftPatternTokenizer : PatternTokenizer
{
    private static readonly char[] TokenChars = ['n', 'a', 'c', 'e'];

    internal SwiftPatternTokenizer()
        : base(ch => ch.IsUpperAsciiLetter() || TokenChars.Contains(ch))
    {
    }

    protected override AsciiCategory GetCategory(string token)
    {
        if (token.Length <= 1)
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

    protected override int GetLength(string token, AsciiCategory category, out bool isFixedLength)
    {
        if (category == AsciiCategory.None)
        {
            isFixedLength = true;
            return -1;
        }

        string lengthDescriptor = token.Substring(0, token.Length - 1);
        // ReSharper disable once UseIndexFromEndExpression
        isFixedLength = lengthDescriptor[lengthDescriptor.Length - 1] == '!';
        return int.Parse(
            lengthDescriptor.Substring(0, lengthDescriptor.Length - Convert.ToByte(isFixedLength)),
            NumberStyles.None,
            CultureInfo.InvariantCulture
        );
    }
}

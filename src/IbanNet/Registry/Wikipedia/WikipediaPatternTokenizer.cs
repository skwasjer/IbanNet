using System.Globalization;
using IbanNet.Extensions;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Wikipedia;

internal class WikipediaPatternTokenizer : PatternTokenizer
{
    private static readonly char[] TokenChars = ['n', 'a', 'c'];

    public WikipediaPatternTokenizer()
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
            _ => AsciiCategory.None
        };
    }

    protected override int GetLength(string token, AsciiCategory category, out bool isFixedLength)
    {
        isFixedLength = true;
        if (category == AsciiCategory.None)
        {
            return -1;
        }


        return int.Parse(
#if USE_SPANS
            token.AsSpan(0, token.Length - 1),
#else
            token.Substring(0, token.Length - 1),
#endif
            NumberStyles.None,
            CultureInfo.InvariantCulture
        );
    }
}

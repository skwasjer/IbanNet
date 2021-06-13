using System.Globalization;
using System.Linq;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Wikipedia
{
    internal class WikipediaPatternTokenizer : PatternTokenizer
    {
        private static readonly char[] TokenChars = { 'n', 'a', 'c' };

        public WikipediaPatternTokenizer() : base(TokenChars.Contains)
        {
        }

        protected override AsciiCategory GetCategory(string token)
        {
            if (token.Length < 2)
            {
                return AsciiCategory.Other;
            }

            // ReSharper disable once UseIndexFromEndExpression
            char tokenChar = token[token.Length - 1];
            return tokenChar switch
            {
                'n' => AsciiCategory.Digit,
                'a' => AsciiCategory.UppercaseLetter,
                'c' => AsciiCategory.AlphaNumeric,
                _ => AsciiCategory.Other
            };
        }

        protected override int GetLength(string token, out bool isFixedLength)
        {
            isFixedLength = true;
            if (token.Length < 2)
            {
                return -1;
            }

            return int.Parse(
                token.Substring(0, token.Length - 1),
                NumberStyles.None,
                CultureInfo.InvariantCulture
            );
        }
    }
}

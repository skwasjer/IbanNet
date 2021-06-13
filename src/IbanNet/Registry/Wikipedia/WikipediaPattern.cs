using System;
using System.Linq;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Wikipedia
{
    internal class WikipediaPattern : Pattern
    {
        private static readonly WikipediaPatternTokenizer _tokenizer = new WikipediaPatternTokenizer();

        public WikipediaPattern(string pattern) : base(pattern?.Replace(",", "")!, _tokenizer)
        {
        }

        public override string ToString()
        {
            return string.Join(",", Tokens.Select(t => $"{t.MaxLength}{GetToken(t.Category)}"));
        }

        private static char GetToken(AsciiCategory category)
        {
            return category switch
            {
                AsciiCategory.Digit => 'n',
                AsciiCategory.UppercaseLetter => 'a',
                AsciiCategory.AlphaNumeric => 'c',
                _ => throw new InvalidOperationException()
            };
        }
    }
}

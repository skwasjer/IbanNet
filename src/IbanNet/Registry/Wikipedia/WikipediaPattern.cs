using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Wikipedia;

internal class WikipediaPattern : Pattern
{
    private static readonly WikipediaPatternTokenizer Tokenizer = new();

    public WikipediaPattern(string pattern) : base(
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
            pattern?.Replace(",", null, StringComparison.Ordinal)!,
#else
        pattern?.Replace(",", null)!,
#endif
        Tokenizer
    )
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
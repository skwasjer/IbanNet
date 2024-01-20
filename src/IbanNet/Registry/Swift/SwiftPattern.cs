using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Swift;

/// <inheritdoc />
#if DEBUG
public
#else
internal
#endif
    class SwiftPattern : Pattern
{
    private static readonly SwiftPatternTokenizer Tokenizer = new();

    /// <inheritdoc />
    public SwiftPattern(string pattern) : base(pattern, Tokenizer)
    {
    }

    internal SwiftPattern(IEnumerable<PatternToken> tokens) : base(tokens)
    {
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Join("", Tokens.Select(t =>
        {
            if (t.Value is not null)
            {
                return t.Value;
            }

            string fixedLen = t.IsFixedLength ? "!" : string.Empty;
            return $"{t.MaxLength}{fixedLen}{GetToken(t.Category)}";
        }));
    }

    private static char GetToken(AsciiCategory category)
    {
        return category switch
        {
            AsciiCategory.Digit => 'n',
            AsciiCategory.UppercaseLetter => 'a',
            AsciiCategory.AlphaNumeric => 'c',
            AsciiCategory.Space => 'e',
            _ => throw new InvalidOperationException()
        };
    }
}

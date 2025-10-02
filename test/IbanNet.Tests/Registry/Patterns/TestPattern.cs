namespace IbanNet.Registry.Patterns;

internal sealed class TestPattern : Pattern
{
    private readonly string? _pattern;

    public TestPattern(string pattern, ITokenizer<PatternToken> tokenizer) : base(pattern, tokenizer)
    {
        _pattern = pattern;
    }

    public TestPattern(IEnumerable<PatternToken> tokens) : base(tokens)
    {
    }

    public TestPattern(string pattern, int maxLength, bool isFixedLength, PatternToken[] tokens)
        : base(pattern, maxLength, isFixedLength, tokens)
    {
        _pattern = pattern;
    }

    public TestPattern(string pattern, PatternToken[] tokens)
        : base(tokens)
    {
        _pattern = pattern;
    }

    public override string ToString()
    {
        return _pattern ?? base.ToString();
    }
}

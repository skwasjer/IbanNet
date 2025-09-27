namespace IbanNet.Registry.Patterns;

internal sealed class TestPattern : Pattern
{
    public TestPattern(string pattern, ITokenizer<PatternToken> tokenizer) : base(pattern, tokenizer)
    {
    }

    public TestPattern(IEnumerable<PatternToken> tokens) : base(tokens)
    {
    }
}

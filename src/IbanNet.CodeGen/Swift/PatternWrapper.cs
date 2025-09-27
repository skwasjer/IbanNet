using IbanNet.Registry.Patterns;
using IbanNet.Registry.Swift;

namespace IbanNet.CodeGen.Swift;

internal sealed class PatternWrapper(string pattern, ITokenizer<PatternToken> tokenizer) : Pattern(pattern, tokenizer)
{
    private string? _pattern;

    public override string ToString()
    {
        return _pattern ??= SwiftPattern.Format(Tokens);
    }
}

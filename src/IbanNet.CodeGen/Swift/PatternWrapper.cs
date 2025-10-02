using IbanNet.Registry.Patterns;

namespace IbanNet.CodeGen.Swift;

internal sealed class PatternWrapper(string pattern, ITokenizer<PatternToken> tokenizer) : Pattern(pattern, tokenizer);

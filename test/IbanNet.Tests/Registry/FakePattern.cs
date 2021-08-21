using System.Collections.Generic;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry
{
    internal class FakePattern : Pattern
    {
        public FakePattern(string pattern, ITokenizer<PatternToken> tokenizer) : base(pattern, tokenizer)
        {
        }

        public FakePattern(IEnumerable<PatternToken> tokens) : base(tokens)
        {
        }
    }
}

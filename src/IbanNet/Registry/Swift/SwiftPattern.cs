using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Swift
{
    internal class SwiftPattern : Pattern
    {
        private static readonly SwiftPatternTokenizer _tokenizer = new SwiftPatternTokenizer();

        public SwiftPattern(string pattern) : base(pattern, _tokenizer)
        {
        }
    }
}

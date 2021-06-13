using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Swift
{
    internal class SwiftPattern : Pattern
    {
        private static readonly SwiftPatternTokenizer Tokenizer = new();

        public SwiftPattern(string pattern) : base(pattern, Tokenizer)
        {
        }
    }
}

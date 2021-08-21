using System.Collections.Generic;
using IbanNet.Extensions;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Swift
{
    internal class IbanSwiftPattern : SwiftPattern
    {
        private const int CountryCodeLength = 2;

        public IbanSwiftPattern(string pattern) : base(AdjustPattern(pattern)!)
        {
        }

        internal IbanSwiftPattern(IEnumerable<PatternToken> tokens) : base(tokens)
        {
        }

        private static string? AdjustPattern(string? pattern)
        {
            return pattern is null || !(pattern.Length >= CountryCodeLength && pattern[0].IsAsciiLetter() && pattern[1].IsAsciiLetter())
                ? null
                : "2!a" + pattern.Substring(2);
        }
    }
}

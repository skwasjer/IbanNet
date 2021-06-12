using IbanNet.Extensions;

namespace IbanNet.Registry.Swift
{
    internal class IbanSwiftPattern : SwiftPattern
    {
        private const int CountryCodeLength = 2;

        public IbanSwiftPattern(string pattern) : base(AdjustPattern(pattern)!)
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

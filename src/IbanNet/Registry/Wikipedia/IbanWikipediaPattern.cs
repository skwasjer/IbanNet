namespace IbanNet.Registry.Wikipedia
{
    internal class IbanWikipediaPattern : WikipediaPattern
    {
        public IbanWikipediaPattern(string pattern)
            : base(AdjustPattern(pattern)!)
        {
        }

        private static string? AdjustPattern(string? pattern)
        {
            return pattern is null
                ? null
                : "2a,2n" + pattern;
        }
    }
}

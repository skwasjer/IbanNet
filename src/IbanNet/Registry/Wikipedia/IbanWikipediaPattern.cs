namespace IbanNet.Registry.Wikipedia;

internal class IbanWikipediaPattern : WikipediaPattern
{
    public IbanWikipediaPattern(string countryCode, string pattern)
        : base(countryCode + ",2n," + pattern)
    {
    }

    public override string ToString()
    {
        // strip country code, check digits tokens and separators, eg.: None[2], comma, Digit[2], comma.
        return base.ToString().Substring(6);
    }
}

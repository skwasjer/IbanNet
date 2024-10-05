using System.Globalization;

namespace IbanNet.CodeGen.Wikipedia;

public record WikiRecord
{
    public string CountryCode { get; init; } = default!;
    public string EnglishName { get; init; } = default!;
    public string Pattern { get; init; } = default!;

    public string? NativeName
    {
        get
        {
            try
            {
                return new RegionInfo(CountryCode).NativeName;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}

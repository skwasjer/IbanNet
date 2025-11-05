using CsvHelper;
using CsvHelper.Configuration;

namespace IbanNet.CodeGen.Swift.Converters;

internal class SanitizedCountryCodeListConverter : CommaSeparatedEnumerableConverter
{
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        var countryCodes = (List<string>)base.ConvertFromString(text, row, memberMapData);
        for (int i = 0; i < countryCodes.Count; i++)
        {
            countryCodes[i] = countryCodes[i].Substring(0, 2).ToUpperInvariant();
        }
        return countryCodes;
    }
}

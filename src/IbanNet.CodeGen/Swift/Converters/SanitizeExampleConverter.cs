using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace IbanNet.CodeGen.Swift.Converters;

internal class SanitizeExampleConverter : StringConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        string? value = base.ConvertFromString(text, row, memberMapData) as string;
        return string.IsNullOrEmpty(value)
            ? value
            : Regex.Replace(value, @"[^\w]", "");
    }
}

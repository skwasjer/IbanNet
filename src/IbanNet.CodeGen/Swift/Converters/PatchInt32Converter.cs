using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace IbanNet.CodeGen.Swift.Converters;

internal class PatchInt32Converter : Int32Converter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        // Some CSV values are broken, fix them.
        return base.ConvertFromString(Regex.Replace(text, @"[^\d]", ""), row, memberMapData);
    }
}

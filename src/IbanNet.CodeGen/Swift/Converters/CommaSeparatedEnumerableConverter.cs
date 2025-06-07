using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace IbanNet.CodeGen.Swift.Converters;

internal class CommaSeparatedEnumerableConverter : StringConverter
{
    private static readonly char[] Separator = [','];

    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (base.ConvertFromString(text, row, memberMapData) is string value)
        {
            return value
                .Split(Separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList();
        }

        return new List<string>();
    }

    public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        throw new NotSupportedException();
    }
}

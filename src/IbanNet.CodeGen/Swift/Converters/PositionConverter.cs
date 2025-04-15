using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace IbanNet.CodeGen.Swift.Converters;

internal class PositionConverter : StringConverter
{
    private readonly char[] _splitChars = { '-', ' ' };

    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (base.ConvertFromString(text, row, memberMapData) is not string value)
        {
            return null;
        }

        string[] segments = value.Split(_splitChars, StringSplitOptions.RemoveEmptyEntries);
        int startPos = int.Parse(segments[0], NumberFormatInfo.InvariantInfo);
        return segments.Length switch
        {
            0 => throw new InvalidOperationException("Expected positional data."),
            1 => new Position { StartPos = startPos, EndPos = startPos },
            _ => new Position { StartPos = startPos, EndPos = int.Parse(segments[1], NumberFormatInfo.InvariantInfo) }
        };
    }

    public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        throw new NotSupportedException();
    }
}

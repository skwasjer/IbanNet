using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace IbanNet.CodeGen.Swift.Converters;

internal class TrueBooleanConverter : BooleanConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        try
        {
            // Default to false.
            if (memberMapData.Default == null && !memberMapData.UseDefaultOnConversionFailure)
            {
                memberMapData.Default = false;
                memberMapData.UseDefaultOnConversionFailure = true;
            }

            return base.ConvertFromString(text, row, memberMapData);
        }
        catch (TypeConverterException)
        {
            // If not matching any explicit true value, we return false instead of throwing.
            return false;
        }
    }
}

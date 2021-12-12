using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace IbanNet.CodeGen.Swift.Converters
{
    internal class CommaSeparatedEnumerableConverter : StringConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (base.ConvertFromString(text, row, memberMapData) is string value)
            {
                return value
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList();
            }

            return new List<string>();
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            throw new NotSupportedException();
        }
    }
}

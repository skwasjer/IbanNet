namespace IbanNet.CodeGen.Swift.Patches;

/// <summary>
/// r99: Norway BBAN pattern is suddenly prefixed with 'NO', which is wrong.
/// </summary>
internal sealed class _11_NorwayBbanPatternPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        switch (record)
        {
            case { CountryCode: "NO" }:
                if (record.Bban.Pattern?.StartsWith("NO", StringComparison.Ordinal) == true)
                {
                    record.Bban.Pattern = record.Bban.Pattern.Substring(2);
                }

                return record;

            default:
                return record;
        }
    }
}

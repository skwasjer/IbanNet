namespace IbanNet.CodeGen.Swift.Patches;

internal sealed class _3_IraqPatternPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        switch (record)
        {
            case { CountryCode: "IQ" }:
                if (record.Bank.Pattern is not null && char.IsNumber(record.Bank.Pattern.Last()))
                {
                    record.Bank.Pattern += "!a";
                }

                if (record.Branch.Pattern is not null && char.IsNumber(record.Branch.Pattern.Last()))
                {
                    record.Branch.Pattern += "!n";
                }

                return record;

            default:
                return record;
        }
    }
}

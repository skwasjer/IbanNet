namespace IbanNet.CodeGen.Swift.Patches;

internal sealed class _2_EgyptPatternPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        switch (record)
        {
            case { CountryCode: "EG" }:
                if (record.Bank.Pattern is not null && record.Bank.Pattern.Last() == '!')
                {
                    record.Bank.Pattern += "n";
                }

                if (record.Branch.Pattern is not null && record.Branch.Pattern.Last() == '!')
                {
                    record.Branch.Pattern += "n";
                }

                return record;

            default:
                return record;
        }
    }
}

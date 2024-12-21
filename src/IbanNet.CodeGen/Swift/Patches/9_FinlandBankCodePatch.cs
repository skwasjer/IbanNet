namespace IbanNet.CodeGen.Swift.Patches;

internal sealed class _9_FinlandBankCodePatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        switch (record)
        {
            case { CountryCode: "FI" }:
                record.Bban.Pattern = "6!n8!n";
                record.Bban.Example = "12345600000785";
                record.Bank.Pattern = "6!n";
                record.Bank.Example = "123456";

                return record;

            default:
                return record;
        }
    }
}

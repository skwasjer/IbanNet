namespace IbanNet.CodeGen.Swift.Patches;

/// <remarks>
/// - r92: missing bank info
/// - r93: some fixes, but still missing pattern
/// </remarks>
internal sealed class _5_FinlandBankPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        switch (record)
        {
            case { CountryCode: "FI" }:
                record.Bank.Pattern = "3!n";

                return record;

            default:
                return record;
        }
    }
}

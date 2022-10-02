namespace IbanNet.CodeGen.Swift.Patches;

internal sealed class _5_FinlandBankPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        switch (record)
        {
            case { CountryCode: "FI" }:
                // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
                record.Bank ??= new BankCsvData
                {
                    Pattern = "3!n",
                    Position = new Position { StartPos = 4, EndPos = 7 },
                    Example = "123"
                };

                return record;

            default:
                return record;
        }
    }
}

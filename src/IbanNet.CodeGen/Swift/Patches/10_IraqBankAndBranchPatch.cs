namespace IbanNet.CodeGen.Swift.Patches;

/// <summary>
/// r99: Iraq bank/branch position is suddenly borked, sigh. (compared to previous revisions)
/// </summary>
internal sealed class _10_IraqBankAndBranchPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        switch (record)
        {
            case { CountryCode: "IQ" }:
                if (record.Bank.Position?.StartPos + 1 == 42461)
                {
                    record = record with { Bank = record.Bank with { Position = new Position { StartPos = 0, EndPos = 4 } } };
                }

                if (record.Branch.Position?.StartPos + 1 == 42556)
                {
                    record = record with { Branch = record.Branch with { Position = new Position { StartPos = 4, EndPos = 4 + 3 } } };
                }

                return record;

            default:
                return record;
        }
    }
}

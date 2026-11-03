namespace IbanNet.CodeGen.Swift.Patches;

/// <summary>
/// The SWIFT registry r102 has no branch identifier position for Portugal, while r99 defined it
/// (BBAN positions 5-8, pattern 4!n, example 0123). The BBAN structure (4!n4!n11!n2!n) is unchanged
/// and consists of bank (4), branch (4), account number (11) and national check digits (2).
/// See https://github.com/skwasjer/IbanNet/issues/448
/// </summary>
internal class _6_PortugalBranchPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        if (record is { CountryCode: "PT", Branch.Pattern: null })
        {
            int branchStartPos = record.Bank.Position!.Value.EndPos;
            return record with
            {
                Branch = record.Branch with
                {
                    Pattern = "4!n",
                    Position = new Position
                    {
                        StartPos = branchStartPos,
                        EndPos = branchStartPos + 4
                    },
                    Example = "0123"
                }
            };
        }
        else
        {
            return record;
        }
    }
}

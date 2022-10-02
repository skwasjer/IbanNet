namespace IbanNet.CodeGen.Swift.Patches;

/// <summary>
/// See https://github.com/skwasjer/IbanNet/issues/77
/// </summary>
internal class _1_FranceBranchPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        if (record is { CountryCode: "FR" })
        {
            int branchStartPos = record.Bank.Position!.Value.EndPos + 1;
            return record with
            {
                Branch = record.Branch with
                {
                    Pattern = "5!n",
                    Position = new Position
                    {
                        StartPos = branchStartPos,
                        EndPos = branchStartPos + 5
                    },
                    Example = "01005"
                }
            };
        }
        else
        {
            return record;
        }
    }
}

namespace IbanNet.CodeGen.Swift.Patches;

/// <remarks>
/// - r93: Iceland is member of SEPA
/// </remarks>
internal sealed class _7_IcelandSepaPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        switch (record)
        {
            case { CountryCode: "IS" }:
                if (!record.Sepa.IsMember)
                {
                    record.Sepa.IsMember = true;
                };

                return record;

            default:
                return record;
        }
    }
}

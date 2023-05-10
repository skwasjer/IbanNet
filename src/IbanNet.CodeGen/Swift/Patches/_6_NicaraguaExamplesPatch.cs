namespace IbanNet.CodeGen.Swift.Patches;

/// <remarks>
/// - r94: example has an invalid check digit... :x
/// </remarks>
internal sealed class _6_NicaraguaExamplesPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        switch (record)
        {
            case { CountryCode: "NI" }:
                if (record.Iban.ElectronicFormatExample == "NI04BAPR00000013000003558124")
                {
                    record.Iban.ElectronicFormatExample = "NI45BAPR00000013000003558124";
                }

                return record;

            default:
                return record;
        }
    }
}

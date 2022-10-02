namespace IbanNet.CodeGen.Swift.Patches;

internal sealed class _4_SoaTomeExamplesPatch : RecordPatcher
{
    protected override SwiftCsvRecord Apply(SwiftCsvRecord record)
    {
        switch (record)
        {
            case { CountryCode: "ST" }:
                if (record.Iban.ElectronicFormatExample == "ST68000200010192194210112")
                {
                    record.Iban.ElectronicFormatExample = "ST23000100010051845310146";
                }

                if (record.Bban.Example == "000200010192194210112")
                {
                    record.Bban.Example = "000100010051845310146";
                }

                if (record.DomesticExample == "01921942101.12")
                {
                    record.DomesticExample = "0051845310146";
                }

                return record;

            default:
                return record;
        }
    }
}

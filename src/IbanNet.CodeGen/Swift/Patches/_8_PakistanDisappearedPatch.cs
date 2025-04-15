namespace IbanNet.CodeGen.Swift.Patches;

/// <summary>
/// Somewhere between r95 and r98, Pakistan disappeared from the txt while it is still is documented in the PDF.
/// I am starting to hate the interns at SWIFT. This patches it back in.
/// </summary>
internal sealed record _8_PakistanDisappearedPatch : SwiftCsvRecord
{
    public _8_PakistanDisappearedPatch()
    {
        CountryCode = "PK";
        EnglishName = "Pakistan";
        Iban = new IbanCsvData
        {
            Pattern = "PK2!n4!a16!c",
            Length = 24,
            ElectronicFormatExample = "PK36SCBL0000001123456702"
        };
        EffectiveDate = new DateTimeOffset(2012, 12, 1, 0, 0, 0, TimeSpan.Zero);
        Bban = new BbanCsvData
        {
            Pattern = "4!a16!c",
            Length = 20,
            Example = "SCBL0000001123456702"
        };
        Bank = new BankCsvData
        {
            Pattern = "4!a",
            Position = new Position { StartPos = 1, EndPos = 4 },
            Example = "SCBL"
        };
        Branch = new BranchCsvData();
        Sepa = new SepaCsvData { IsMember = false, OtherTerritories = [] };
        OtherTerritories = [];
        DomesticExample = "00260101036360";
        LastUpdatedDate = new DateTimeOffset(2012, 12, 1, 0, 0, 0, TimeSpan.Zero);
    }
}

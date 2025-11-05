using System.Globalization;
using CsvHelper.Configuration.Attributes;
using IbanNet.CodeGen.Swift.Converters;
using IbanNet.Registry.Patterns;

namespace IbanNet.CodeGen.Swift;

public record SwiftCsvRecord
{
    private string _countryCode = default!;

    [Name("Name of country")]
    public string EnglishName { get; set; } = default!;

    [Name("IBAN prefix country code (ISO 3166)")]
    public string CountryCode
    {
        get => _countryCode;
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        set => _countryCode = value?.ToUpperInvariant() ?? throw new ArgumentNullException(nameof(value));
    }

    [Ignore]
    public string? NativeName
    {
        get
        {
            try
            {
                return new RegionInfo(CountryCode).NativeName;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }

    [Name("Domestic account number example")]
    //    [TypeConverter(typeof(ExampleConverter))]
    public string? DomesticExample { get; set; }

    public IbanCsvData Iban { get; set; } = default!;

    public BbanCsvData Bban { get; set; } = default!;

    public BankCsvData Bank { get; set; } = default!;

    public BranchCsvData Branch { get; set; } = default!;

    public SepaCsvData Sepa { get; set; } = default!;

    [Name("Country code includes other countries/territories")]
    [TypeConverter(typeof(SanitizedCountryCodeListConverter))]
    [NullValues("N/A")]
#pragma warning disable CA2227 // Collection properties should be read only
    public ICollection<string> OtherTerritories { get; set; } = default!;
#pragma warning restore CA2227 // Collection properties should be read only

    [Name("Effective date")]
    [Format("MMM-yy")]
    [DateTimeStyles(DateTimeStyles.AssumeUniversal)]
    public DateTimeOffset? EffectiveDate { get; set; }

    [Name("Last update date")]
    [Format("MMM-yy")]
    [DateTimeStyles(DateTimeStyles.AssumeUniversal)]
    public DateTimeOffset? LastUpdatedDate { get; set; }
}

public record struct Position
{
    public int StartPos { get; set; }
    public int EndPos { get; set; }
}

public record IbanCsvData
{
    [Name("IBAN structure")]
    public string Pattern { get; set; } = default!;

    [Name("IBAN length")]
    public int Length { get; set; }

    [Name("IBAN electronic format example")]
    public string? ElectronicFormatExample { get; set; }

    [Name("IBAN print format example")]
    public string? PrintFormatExample { get; set; }

    [Ignore]
    public string? Example => ElectronicFormatExample;

    [Ignore]
    public ITokenizer<PatternToken> Tokenizer { get; set; } = null!;
}

public abstract record PatternCsvData
{
    public virtual string? Pattern { get; set; }

    public virtual Position? Position { get; set; }

    [TypeConverter(typeof(SanitizeExampleConverter))]
    public virtual string? Example { get; set; }

    [Ignore]
    public ITokenizer<PatternToken> Tokenizer { get; set; } = null!;
}

public record BbanCsvData : PatternCsvData
{
    [Name("BBAN structure")]
    public override string? Pattern { get; set; }

    [Name("BBAN length")]
    public int Length { get; set; }

    [Ignore]
    public override Position? Position
    {
        get => new Position { StartPos = 4, EndPos = Length + 4 };
        set { }
    }

    [Name("BBAN example")]
    public override string? Example { get; set; }
}

public record BankCsvData : PatternCsvData
{
    [Name("Bank identifier pattern")]
    public override string? Pattern { get; set; }

    [Name("Bank identifier position within the BBAN")]
    public override Position? Position { get; set; }

    [Name("Bank identifier example")]
    public override string? Example { get; set; }
}

public record BranchCsvData : PatternCsvData
{
    [Name("Branch identifier pattern")]
    public override string? Pattern { get; set; }

    [Name("Branch identifier position within the BBAN")]
    public override Position? Position { get; set; }

    [Name("Branch identifier example")]
    public override string? Example { get; set; }
}

public record SepaCsvData
{
    [Name("SEPA country")]
    [BooleanTrueValues("Yes")]
    [BooleanFalseValues("No")]
    public bool IsMember { get; set; }

    [Name("SEPA country also includes")]
    [TypeConverter(typeof(SanitizedCountryCodeListConverter))]
    [NullValues("N/A")]
#pragma warning disable CA2227 // Collection properties should be read only
    public ICollection<string> OtherTerritories { get; set; } = default!;
#pragma warning restore CA2227 // Collection properties should be read only
}

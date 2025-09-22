using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace IbanNet.Registry;

/// <summary>
/// Contains IBAN/BBAN format information about the country.
/// </summary>
[DebuggerDisplay("\\{{" + nameof(TwoLetterISORegionName) + ",nq} - {" + nameof(EnglishName) + ",nq}\\}")]
[DebuggerStepThrough]
public sealed class IbanCountry
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string? _displayName;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IbanStructure? _ibanStructure;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private BbanStructure? _bbanStructure;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private BankStructure? _bankStructure;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private BranchStructure? _branchStructure;
    private readonly DateTimeOffset? _effectiveDate;

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanCountry" /> class using specified 2 letter ISO region name.
    /// </summary>
    /// <param name="twoLetterISORegionName">The 2 letter iso region name.</param>
    // ReSharper disable once InconsistentNaming
    public IbanCountry(string twoLetterISORegionName)
    {
        if (twoLetterISORegionName is null)
        {
            throw new ArgumentNullException(nameof(twoLetterISORegionName));
        }

        if (twoLetterISORegionName.Length != 2)
        {
            throw new ArgumentException(Resources.ArgumentException_Invalid_country_code, nameof(twoLetterISORegionName));
        }

        TwoLetterISORegionName = twoLetterISORegionName.ToUpperInvariant();
        EnglishName = TwoLetterISORegionName;
    }

    /// <summary>
    /// Gets the country code.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public string TwoLetterISORegionName { get; }

    /// <summary>
    /// Gets the display name. If no value is set on init, then returns the <see cref="NativeName" />, if available; otherwise returns the <see cref="EnglishName" />.
    /// </summary>
    public string DisplayName
    {
        get => _displayName ?? NativeName ?? EnglishName;
        init => _displayName = value;
    }

    /// <summary>
    /// Gets the native name, if available.
    /// </summary>
    public string? NativeName { get; init; }

    /// <summary>
    /// Gets the English name.
    /// </summary>
    public string EnglishName { get; init; }

    /// <summary>
    /// Gets the list of included countries.
    /// </summary>
    public IReadOnlyCollection<string> IncludedCountries { get; init; } = new ReadOnlyCollection<string>([]);

    /// <summary>
    /// Gets SEPA information.
    /// </summary>
    public SepaInfo? Sepa { get; init; }

    /// <summary>
    /// Gets a domestic account number example.
    /// </summary>
    public string? DomesticAccountNumberExample { get; init; }

    /// <summary>
    /// Gets the structure of the BBAN.
    /// </summary>
    [AllowNull]
    public BbanStructure Bban
    {
        get => _bbanStructure ??= new BbanStructure(NullPattern.Instance);
        init => _bbanStructure = value;
    }

    /// <summary>
    /// Gets the structure of the IBAN.
    /// </summary>
    [AllowNull]
    public IbanStructure Iban
    {
        get => _ibanStructure ??= new IbanStructure(NullPattern.Instance);
        init => _ibanStructure = value;
    }

    /// <summary>
    /// Gets the bank identifier structure section.
    /// </summary>
    [AllowNull]
    public BankStructure Bank
    {
        get => _bankStructure ??= new BankStructure(NullPattern.Instance);
        init => _bankStructure = value;
    }

    /// <summary>
    /// Gets the branch identifier structure section.
    /// </summary>
    [AllowNull]
    public BranchStructure Branch
    {
        get => _branchStructure ??= new BranchStructure(NullPattern.Instance);
        init => _branchStructure = value;
    }

    /// <summary>
    /// Gets when this <see cref="IbanCountry" /> was last updated in the Iban Registry.
    /// </summary>
    public DateTimeOffset LastUpdatedDate { get; init; }

    /// <summary>
    /// Gets the date the IBAN came in effect.
    /// </summary>
    public DateTimeOffset EffectiveDate
    {
#pragma warning disable CS0618 // Type or member is obsolete
        get => _effectiveDate ?? Iban.EffectiveDate;
#pragma warning restore CS0618 // Type or member is obsolete
        init => _effectiveDate = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return TwoLetterISORegionName;
    }
}

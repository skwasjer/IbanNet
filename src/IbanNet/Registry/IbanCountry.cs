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
    private static readonly PatternDescriptor NullPatternDescriptor = new(NullPattern.Instance);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string? _displayName;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly PatternDescriptor? _ibanPatternDescriptor;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly PatternDescriptor? _bbanPatternDescriptor;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly PatternDescriptor? _bankPatternDescriptor;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly PatternDescriptor? _branchPatternDescriptor;

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
    /// Gets the pattern descriptor for the BBAN.
    /// </summary>
    [AllowNull]
    public PatternDescriptor Bban
    {
        get => _bbanPatternDescriptor ?? NullPatternDescriptor;
        init => _bbanPatternDescriptor = value;
    }

    /// <summary>
    /// Gets the pattern descriptor for the IBAN.
    /// </summary>
    [AllowNull]
    public PatternDescriptor Iban
    {
        get => _ibanPatternDescriptor ?? NullPatternDescriptor;
        init => _ibanPatternDescriptor = value;
    }

    /// <summary>
    /// Gets the pattern descriptor for the bank identifier.
    /// </summary>
    [AllowNull]
    public PatternDescriptor Bank
    {
        get => _bankPatternDescriptor ?? NullPatternDescriptor;
        init => _bankPatternDescriptor = value;
    }

    /// <summary>
    /// Gets the pattern descriptor for the branch identifier.
    /// </summary>
    [AllowNull]
    public PatternDescriptor Branch
    {
        get => _branchPatternDescriptor ?? NullPatternDescriptor;
        init => _branchPatternDescriptor = value;
    }

    /// <summary>
    /// Gets when this <see cref="IbanCountry" /> was last updated in the Iban Registry.
    /// </summary>
    public DateTimeOffset LastUpdatedDate { get; init; }

    /// <summary>
    /// Gets the date the IBAN came in effect.
    /// </summary>
    public DateTimeOffset EffectiveDate { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        return TwoLetterISORegionName;
    }
}

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using IbanNet.Extensions;
using IbanNet.Internal;
using IbanNet.Registry;
using IbanNet.TypeConverters;

namespace IbanNet;

/// <summary>
/// Represents an IBAN.
/// </summary>
[TypeConverter(typeof(IbanTypeConverter))]
#if NET8_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
[System.Text.Json.Serialization.JsonConverter(typeof(Json.IbanJsonConverter))]
#endif
public sealed class Iban
    : IEquatable<Iban>,
#if NET8_0_OR_GREATER
      ISpanParsable<Iban>,
#endif
      IFormattable
{
    /// <summary>
    /// The maximum length of any IBAN, from any country.
    /// </summary>
    /// <remarks>Note: This is the maximum length as per the spec. No IBAN should be longer than the value provided by this constant. Thus, it can be used for example to allocate a buffer, truncate input that must still be validated, or as a global pre-validation constraint (eg. when accepting IBAN's from multiple countries).
    /// <para>To get the actual maximum length for a specific country, use the <see cref="IIbanRegistry" /> to get the country specific configuration.</para></remarks>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public const int MaxLength = 34;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly Func<IIbanValidator> DefaultFactory = () => new IbanValidator();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string _iban;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static Lazy<IIbanValidator> _validatorInstance = new(
        DefaultFactory,
        LazyThreadSafetyMode.ExecutionAndPublication
    );

    private static readonly string HiddenFormat = new('*', 4);

    /// <summary>
    /// Gets or sets the <see cref="IIbanValidator" /> used to validate an IBAN.
    /// <para>
    /// Note: avoid using this member, it's only use case is allowing type conversion and may be obsolete in future.
    /// </para>
    /// </summary>
    [AllowNull]
    public static IIbanValidator Validator
    {
        get => _validatorInstance.Value;
        set => _validatorInstance = value is null
            ? new Lazy<IIbanValidator>(DefaultFactory, true)
            : new Lazy<IIbanValidator>(() => value, true);
    }

    internal Iban(string iban, IbanCountry ibanCountry, bool skipNormalize = false)
    {
        _iban = (skipNormalize
                ? iban
                : InputNormalization.NormalizeOrNull(iban)
            ) ?? throw new ArgumentNullException(nameof(iban));
        Country = ibanCountry ?? throw new ArgumentNullException(nameof(ibanCountry));
    }

    /// <summary>
    /// Gets the country.
    /// </summary>
    public IbanCountry Country { get; }

    /// <summary>
    /// Gets the BBAN part of the IBAN.
    /// </summary>
    public string Bban => Extract(Country.Bban) ?? _iban.Substring(4);

    /// <summary>
    /// Gets the bank identifier, or <see langword="null" /> if bank identifier cannot be extracted.
    /// </summary>
    public string? BankIdentifier => Extract(Country.Bank);

    /// <summary>
    /// Gets the branch identifier, or <see langword="null" /> if branch identifier cannot be extracted.
    /// </summary>
    public string? BranchIdentifier => Extract(Country.Branch);

    /// <summary>Returns a string that represents the current <see cref="Iban" />.</summary>
    /// <example>
    /// <see cref="IbanFormat.Print" /> =&gt; NL91 ABNA 0417 1643 00
    /// <see cref="IbanFormat.Electronic" /> =&gt; NL91ABNA0417164300
    /// <see cref="IbanFormat.Obfuscated" /> =&gt; XXXXXXXXXXXXXXXXXX4300
    /// <see cref="IbanFormat.Hidden" /> =&gt; ****
    /// </example>
    /// <param name="format">The format to use.</param>
    /// <returns>A string that represents the current <see cref="Iban" />.</returns>
    public string ToString(IbanFormat format)
    {
        const int visibleChars = 4;
        const int segmentSize = 4;
        return format switch
        {
            IbanFormat.Electronic => _iban,
            IbanFormat.Obfuscated =>
#if NET8_0_OR_GREATER
                string.Concat(new string('X', _iban.Length - visibleChars), _iban.AsSpan(_iban.Length - visibleChars, visibleChars)),
#else
                new string('X', _iban.Length - visibleChars) + _iban.Substring(_iban.Length - visibleChars, visibleChars),
#endif
            IbanFormat.Print => string.Join(" ",
                _iban
                    .Chunk(segmentSize)
                    .Select(p => new string(p.ToArray()))
            ),
            IbanFormat.Hidden => HiddenFormat,
            // TODO: change to IbanFormatException in future major release.
            _ => throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ArgumentException_The_format_0_is_invalid, format), nameof(format))
        };
    }

    /// <summary>Returns a string that represents the current <see cref="Iban" /> in electronic format.</summary>
    /// <returns>A string that represents the current <see cref="Iban" /> in electronic format.</returns>
    public override string ToString()
    {
        return ((IFormattable)this).ToString(null, null);
    }

    /// <summary>Formats the value of the current instance using the specified format.</summary>
    /// <example>
    /// P =&gt; NL91 ABNA 0417 1643 00
    /// E =&gt; NL91ABNA0417164300
    /// O =&gt; XXXXXXXXXXXXXXXXXX4300
    /// H =&gt; ****
    /// </example>
    /// <param name="format">The format to use.
    /// <list type="table">
    /// <item>
    /// <term>P</term>
    /// <description>Format in human readable format. (eg. NL91 ABNA 0417 1643 00)
    /// <remarks>
    /// <para>Typically used for displaying IBAN's to end-users in safe environments.</para>
    /// </remarks>
    /// </description>
    /// </item>
    /// <item>
    /// <term>E</term>
    /// <description>Format in electronic format. (eg. NL91ABNA0417164300). This is also the default format in case <paramref name="format"/> is <see langword="null" />.
    /// <remarks>
    /// <para>Typically used for electronic processing and storage of IBAN's.</para>
    /// </remarks>
    /// </description>
    /// </item>
    /// <item>
    /// <term>O</term>
    /// <description>Format in obfuscated format. (eg. XXXXXXXXXXXXXXXXXX4300)
    /// <remarks>
    /// <para>
    /// Typically used when displaying IBAN's in user interfaces to prevent exposing sensitive information.
    /// Note that this format still allows for partial identification of the IBAN, both due to the visible last characters and the length.
    /// </para>
    /// </remarks>
    /// </description>
    /// </item>
    /// <item>
    /// <term>H</term>
    /// <description>Format by hiding the IBAN in its entirety. (eg.: ****)
    /// <remarks>
    /// <para>
    /// Typically used when IBAN's should not be displayed at all, and only an indication that an IBAN exists is needed. (For example in audit logs or activity feeds)
    /// </para>
    /// </remarks>
    /// </description>
    /// </item>
    /// </list>
    /// -or-
    /// A <see langword="null"/> reference to use the default format defined for the type of the <see cref="IFormattable" /> implementation.
    /// </param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(string? format)
    {
        return ((IFormattable)this).ToString(format, null);
    }

    string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
    {
        return (format ?? "E").ToUpperInvariant() switch
        {
            "E" => ToString(IbanFormat.Electronic),
            "O" => ToString(IbanFormat.Obfuscated),
            "P" => ToString(IbanFormat.Print),
            "H" => ToString(IbanFormat.Hidden),
            _ => throw new IbanFormatException(string.Format(CultureInfo.CurrentCulture, Resources.ArgumentException_The_format_0_is_invalid, format))
        };
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
    public bool Equals(Iban? other)
    {
        return string.Equals(_iban, other?._iban, StringComparison.Ordinal);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns><see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj is Iban iban && Equals(iban);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
#if NETSTANDARD2_1 || NET8_0_OR_GREATER
        return _iban.GetHashCode(StringComparison.Ordinal);
#else
        return _iban.GetHashCode();
#endif
    }

    /// <summary>
    /// Determines whether the <see cref="Iban" />s are equal to each other.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Iban left, Iban right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the <see cref="Iban" />s are unequal to each other.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Iban left, Iban right)
    {
        return !Equals(left, right);
    }

    private string? Extract(PatternDescriptor? patternDescriptor)
    {
        if (patternDescriptor?.Pattern is null or NullPattern)
        {
            return null;
        }

        if (patternDescriptor.Position + patternDescriptor.Length > _iban.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(patternDescriptor));
        }

        return patternDescriptor.Length == 0
            ? null
#if USE_SPANS
            : _iban.AsSpan(patternDescriptor.Position, patternDescriptor.Length).ToString();
#else
            : _iban.Substring(patternDescriptor.Position, patternDescriptor.Length);
#endif
    }

    /// <summary>
    /// Converts the specified <paramref name="s" /> into an <see cref="Iban" />.
    /// </summary>
    /// <param name="s">The IBAN value to parse.</param>
    /// <returns>An <see cref="Iban" /> if the <paramref name="s" /> is converted successfully.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified <paramref name="s" /> is <see langword="null" />.</exception>
    /// <exception cref="IbanFormatException">Thrown when the specified <paramref name="s" /> is not a valid IBAN.</exception>
    internal static Iban Parse(string s)
    {
        var parser = new IbanParser(IbanRegistry.Default);
        return parser.Parse(s);
    }

#if USE_SPANS
    /// <summary>
    /// Converts the specified <paramref name="s" /> into an <see cref="Iban" />.
    /// </summary>
    /// <param name="s">The IBAN value to parse.</param>
    /// <returns>An <see cref="Iban" /> if the <paramref name="s" /> is converted successfully.</returns>
    /// <exception cref="IbanFormatException">Thrown when the specified <paramref name="s" /> is not a valid IBAN.</exception>
    internal static Iban Parse(ReadOnlySpan<char> s)
    {
        IIbanSpanParser parser = new IbanParser(IbanRegistry.Default);
        return parser.Parse(s);
    }
#endif

#if NET8_0_OR_GREATER
    /// <summary>
    /// Converts the specified <paramref name="s" /> into an <see cref="Iban" />.
    /// </summary>
    /// <param name="s">The IBAN value to parse.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
    /// <returns>An <see cref="Iban" /> if the <paramref name="s" /> is converted successfully.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified <paramref name="s" /> is <see langword="null" />.</exception>
    /// <exception cref="IbanFormatException">Thrown when the specified <paramref name="s" /> is not a valid IBAN.</exception>
    static Iban IParsable<Iban>.Parse(string s, IFormatProvider? provider)
        => Parse(s);

    static Iban ISpanParsable<Iban>.Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        => Parse(s);
#endif

    /// <summary>
    /// Tries to convert the specified <paramref name="s" /> into an <see cref="Iban" />.
    /// </summary>
    /// <param name="s">The IBAN value to parse.</param>
    /// <param name="result">The <see cref="Iban" /> if the <paramref name="s" /> is converted successfully.</param>
    /// <returns><see langword="true" /> if the <paramref name="s" /> is converted successfully, or <see langword="false" /> otherwise</returns>
    internal static bool TryParse(string? s, [NotNullWhen(true)] out Iban? result)
    {
        var parser = new IbanParser(IbanRegistry.Default);
        return parser.TryParse(s, out result);
    }

#if USE_SPANS
    /// <summary>
    /// Tries to convert the specified <paramref name="s" /> into an <see cref="Iban" />.
    /// </summary>
    /// <param name="s">The IBAN value to parse.</param>
    /// <param name="result">The <see cref="Iban" /> if the <paramref name="s" /> is converted successfully.</param>
    /// <returns><see langword="true" /> if the <paramref name="s" /> is converted successfully, or <see langword="false" /> otherwise</returns>
    internal static bool TryParse(ReadOnlySpan<char> s, [NotNullWhen(true)] out Iban? result)
    {
        IIbanSpanParser parser = new IbanParser(IbanRegistry.Default);
        return parser.TryParse(s, out result);
    }
#endif

#if NET8_0_OR_GREATER
    /// <summary>
    /// Tries to convert the specified <paramref name="s" /> into an <see cref="Iban" />.
    /// </summary>
    /// <param name="s">The IBAN value to parse.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
    /// <param name="result">The <see cref="Iban" /> if the <paramref name="s" /> is converted successfully.</param>
    /// <returns><see langword="true" /> if the <paramref name="s" /> is converted successfully, or <see langword="false" /> otherwise</returns>
    static bool IParsable<Iban>.TryParse(string? s, IFormatProvider? provider, [NotNullWhen(true)] out Iban? result)
        => TryParse(s, out result);

    static bool ISpanParsable<Iban>.TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [NotNullWhen(true)] out Iban? result)
        => TryParse(s, out result);
#endif
}

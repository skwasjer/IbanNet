using System.Diagnostics.CodeAnalysis;
using IbanNet.Internal;
using IbanNet.Registry;

namespace IbanNet;

/// <summary>
/// Provides parsing of international bank account numbers into an <see cref="Iban" />.
/// </summary>
public sealed class IbanParser
    : IIbanParser
#if USE_SPANS
      , IIbanSpanParser
#endif
{
    private readonly IIbanValidator _ibanValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanParser" /> class using specified <paramref name="registry" />.
    /// </summary>
    /// <param name="registry">The registry.</param>
    public IbanParser(IIbanRegistry registry)
    {
        if (registry is null)
        {
            throw new ArgumentNullException(nameof(registry));
        }

        _ibanValidator = new IbanValidator(new IbanValidatorOptions { Registry = registry });
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanParser" /> class using specified <paramref name="ibanValidator" />.
    /// </summary>
    /// <param name="ibanValidator"></param>
    public IbanParser(IIbanValidator ibanValidator)
    {
        _ibanValidator = ibanValidator ?? throw new ArgumentNullException(nameof(ibanValidator));
    }

    /// <inheritdoc />
#if USE_SPANS
    public Iban Parse(string value)
    {
        return value is null
            ? throw new ArgumentNullException(nameof(value))
            : ((IIbanSpanParser)this).Parse(value);
    }

    Iban IIbanSpanParser.Parse(ReadOnlySpan<char> value)
    {
#else
    public Iban Parse(string value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
#endif

        if (TryParse(value, out Iban? iban, out ValidationResult validationResult, out Exception? exceptionThrown))
        {
            return iban;
        }

        string errorMessage = validationResult.Error is null || string.IsNullOrEmpty(validationResult.Error.ErrorMessage)
            ? Resources.IbanFormatException_The_value_is_not_a_valid_IBAN
            : validationResult.Error.ErrorMessage;

        if (exceptionThrown is not null)
        {
            throw new IbanFormatException(errorMessage, exceptionThrown);
        }

        throw new IbanFormatException(errorMessage, validationResult);
    }

    /// <inheritdoc />
    public bool TryParse(string? value, [NotNullWhen(true)] out Iban? iban)
    {
        return TryParse(value, out iban, out _, out _);
    }


#if USE_SPANS
    bool IIbanSpanParser.TryParse(ReadOnlySpan<char> value, [NotNullWhen(true)] out Iban? iban)
    {
        return TryParse(value, out iban, out _, out _);
    }
#endif

    private bool TryParse
    (
#if USE_SPANS
        ReadOnlySpan<char> value,
#else
        string? value,
#endif
        [NotNullWhen(true)] out Iban? iban,
        out ValidationResult validationResult,
        // ReSharper disable once RedundantNullableFlowAttribute
        [MaybeNullWhen(false)] out Exception? exceptionThrown)
    {
        iban = null;
        exceptionThrown = null;

#if USE_SPANS
        string normalizedValue = InputNormalization.Normalize(value).ToString();
#else
        string? normalizedValue = InputNormalization.NormalizeOrNull(value);
#endif

        try
        {
            validationResult = _ibanValidator.Validate(normalizedValue);
        }
        catch (Exception ex)
        {
            validationResult = default;
            exceptionThrown = ex;
            return false;
        }

        if (!validationResult.IsValid)
        {
            return false;
        }

        iban = new Iban(normalizedValue!, validationResult.Country!, true);
        return true;
    }
}

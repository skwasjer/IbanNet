using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace IbanNet
{
    /// <summary>
    /// Provides parsing of international bank account numbers into an <see cref="Iban" />.
    /// </summary>
    public sealed class IbanParser : IIbanParser
    {
        private readonly IIbanValidator _ibanValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanParser" /> class using specified <paramref name="ibanValidator" />.
        /// </summary>
        /// <param name="ibanValidator"></param>
        public IbanParser(IIbanValidator ibanValidator)
        {
            _ibanValidator = ibanValidator ?? throw new ArgumentNullException(nameof(ibanValidator));
        }

        /// <inheritdoc />
        public Iban Parse(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (TryParse(value, out Iban? iban, out ValidationResult? validationResult, out Exception? exceptionThrown))
            {
                return iban;
            }

            string errorMessage = validationResult?.Error is null || string.IsNullOrEmpty(validationResult.Error.ErrorMessage)
                ? string.Format(CultureInfo.CurrentCulture, Resources.IbanFormatException_The_value_0_is_not_a_valid_IBAN, value)
                : validationResult.Error.ErrorMessage;

            if (validationResult is null || exceptionThrown is not null)
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

        private bool TryParse(
            string? value,
            [NotNullWhen(true)] out Iban? iban,
            [MaybeNullWhen(false)] out ValidationResult? validationResult,
            [MaybeNullWhen(false)] out Exception? exceptionThrown)
        {
            iban = null;
            exceptionThrown = null;

            // Although our validator normalizes too, we can't rely on this fact if other implementations
            // are provided (like mocks, or maybe faster validators). Thus, to ensure this class correctly
            // represents the IBAN value, we normalize inline here and take the penalty.
            string? normalizedValue = Iban.NormalizeOrNull(value);
            try
            {
                validationResult = _ibanValidator.Validate(normalizedValue);
            }
            catch (Exception ex)
            {
                validationResult = null;
                exceptionThrown = ex;
                return false;
            }

            if (!validationResult.IsValid)
            {
                return false;
            }

            iban = new Iban(normalizedValue!);
            return true;
        }
    }
}

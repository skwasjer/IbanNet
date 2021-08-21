using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using IbanNet.Registry;

namespace IbanNet
{
    /// <summary>
    /// Provides parsing of international bank account numbers into an <see cref="Iban" />.
    /// </summary>
    public sealed class IbanParser : IIbanParser
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

            _ibanValidator = new IbanValidator(new IbanValidatorOptions
            {
                Registry = registry
            });
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

            try
            {
                validationResult = _ibanValidator.Validate(value);
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

            iban = new Iban(validationResult.AttemptedValue!, validationResult.Country!);
            return true;
        }
    }
}

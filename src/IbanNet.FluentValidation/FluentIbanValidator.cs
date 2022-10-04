using FluentValidation;
using FluentValidation.Validators;
using IbanNet.Internal;

namespace IbanNet.FluentValidation
{
    /// <summary>
    /// A property validator for international bank account numbers.
    /// </summary>
    public sealed class FluentIbanValidator<T> : PropertyValidator<T, string>
    {
        private readonly IIbanValidator _ibanValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentIbanValidator{T}" /> class using specified validator.
        /// </summary>
        /// <param name="ibanValidator">The IBAN validator to use.</param>
        public FluentIbanValidator(IIbanValidator ibanValidator)
        {
            _ibanValidator = ibanValidator ?? throw new ArgumentNullException(nameof(ibanValidator));
        }

        /// <summary>
        /// Gets or sets whether to perform strict validation. When true, the input must strictly match the IBAN format rules.
        /// When false, whitespace is ignored and strict character casing enforcement is disabled (meaning, the user can input in lower and uppercase). This mode is a bit more forgiving when dealing with user-input. However it does require after successful validation, that you parse the user input with <see cref="IIbanParser" /> to normalize/sanitize the input and to be able to format the IBAN in correct electronic format.
        ///
        /// <para>Default is <see langword="true" />. (this may change in future major release)</para>
        /// </summary>
        public bool Strict { get; init; } = true;

        /// <inheritdoc />
        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return Resources.Not_a_valid_IBAN;
        }

        /// <inheritdoc />
        public override bool IsValid(ValidationContext<T> context, string value)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (value is null)
            {
                return true;
            }

            ValidationResult result = _ibanValidator.Validate(
                Strict
                    ? value
                    : InputNormalization.NormalizeOrNull(value)
            );
            if (result.Error is not null)
            {
                // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
                context?.MessageFormatter.AppendArgument("Error", result.Error);
            }

            return result.IsValid;
        }

        /// <inheritdoc />
        public override string Name => nameof(FluentIbanValidator<object>);
    }
}

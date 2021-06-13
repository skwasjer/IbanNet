using System;
using FluentValidation;
using FluentValidation.Validators;

namespace IbanNet.FluentValidation
{
    /// <summary>
    /// A property validator for international bank account numbers.
    /// </summary>
    public class FluentIbanValidator<T> : PropertyValidator<T, string>
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

        /// <inheritdoc />
        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return Resources.Not_a_valid_IBAN;
        }

        /// <inheritdoc />
        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (value is null!)
            {
                return true;
            }

            ValidationResult result = _ibanValidator.Validate(value);
            if (result.Error is not null)
            {
                // ReSharper disable once ConstantConditionalAccessQualifier
                context?.MessageFormatter.AppendArgument("Error", result.Error);
            }

            return result.IsValid;
        }

        /// <inheritdoc />
        public override string Name => nameof(FluentIbanValidator<object>);
    }
}

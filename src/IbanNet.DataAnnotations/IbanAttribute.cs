using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace IbanNet.DataAnnotations
{
    /// <summary>
    /// When applied to a <see cref="string" /> property or parameter, validates that a valid IBAN is provided.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class IbanAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IbanAttribute" /> class.
        /// </summary>
        public IbanAttribute()
            : base(Resources.IbanAttribute_Invalid)
        {
        }

        /// <inheritdoc />
        protected override System.ComponentModel.DataAnnotations.ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }

            if (!(value is string strValue))
            {
                return base.IsValid(value, validationContext);
            }

            IIbanValidator ibanValidator = GetValidator(validationContext);
            ValidationResult result = ibanValidator.Validate(strValue);
            if (result.IsValid)
            {
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }

            validationContext.Items.Add("Error", result.Error);

            IEnumerable<string>? memberNames = null;
            if (validationContext.MemberName is { })
            {
                memberNames = new[] { validationContext.MemberName };
            }

            return new System.ComponentModel.DataAnnotations.ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
        }

        /// <inheritdoc />
        public override bool RequiresValidationContext => true;

        /// <summary>
        /// Gets the validator from IoC container.
        /// </summary>
        private static IIbanValidator GetValidator(IServiceProvider serviceProvider)
        {
            var ibanValidator = (IIbanValidator?)serviceProvider?.GetService(typeof(IIbanValidator));
            if (ibanValidator is null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.IbanAttribute_ValidatorMissing, nameof(IIbanValidator)));
            }

            return ibanValidator;
        }
    }
}

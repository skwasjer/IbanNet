using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IbanNet.DataAnnotations
{
	/// <summary>
	/// When applied to a <see cref="string" /> property, field or parameter, validates that a valid IBAN is provided.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
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
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return ValidationResult.Success;
			}

			if (!(value is string strValue))
			{
				return base.IsValid(value, validationContext);
			}

			IIbanValidator ibanValidator = GetValidator(validationContext);
			IbanValidationResult result = ibanValidator.Validate(strValue);
			if (result == IbanValidationResult.Valid)
			{
				return ValidationResult.Success;
			}

			IEnumerable<string> memberNames = null;
			if (validationContext.MemberName != null)
			{
				memberNames = new[] { validationContext.MemberName };
			}

			return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
		}

		/// <summary>
		/// Gets the validator from DI, and falls back to the default validator.
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		private static IIbanValidator GetValidator(IServiceProvider serviceProvider)
		{
			IIbanValidator ibanValidator = (IIbanValidator)serviceProvider?.GetService(typeof(IIbanValidator)) ?? Iban.Validator;
			if (ibanValidator == null)
			{
				throw new InvalidOperationException(string.Format(Resources.IbanAttribute_ValidatorMissing, nameof(IIbanValidator)));
			}

			return ibanValidator;
		}
	}
}
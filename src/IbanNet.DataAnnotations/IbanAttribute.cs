using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IbanNet.DataAnnotations
{
	/// <summary>
	/// When applied to a <see cref="string" /> property, validates that a valid IBAN is provided.
	/// </summary>
	public class IbanAttribute : ValidationAttribute
	{
		private readonly IIbanValidator _ibanValidator;

		/// <summary>
		/// </summary>
		public IbanAttribute() : this(Iban.Validator)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="ibanValidator"></param>
		public IbanAttribute(IIbanValidator ibanValidator)
		{
			_ibanValidator = ibanValidator ?? throw new ArgumentNullException(nameof(ibanValidator));
		}

		/// <inheritdoc />
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (validationContext?.ObjectType == typeof(string))
			{
				IbanValidationResult result = _ibanValidator.Validate(value as string);
				if (result == IbanValidationResult.Valid)
				{
					return ValidationResult.Success;
				}

				IEnumerable<string> memberNames = null;
				if (validationContext.MemberName != null)
				{
					memberNames = new[] { validationContext.MemberName };
				}

				return new ValidationResult(ErrorMessageString, memberNames);
			}

			return base.IsValid(value, validationContext);
		}
	}
}
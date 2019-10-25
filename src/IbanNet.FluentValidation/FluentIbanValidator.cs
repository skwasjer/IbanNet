using System;
using FluentValidation.Validators;

namespace IbanNet.FluentValidation
{
	/// <summary>
	/// A property validator for international bank account numbers.
	/// </summary>
	public class FluentIbanValidator : PropertyValidator
	{
		private readonly IIbanValidator _ibanValidator;

		/// <summary>
		/// Initializes a new instance of the <see cref="FluentIbanValidator"/> class using specified validator.
		/// </summary>
		/// <param name="ibanValidator">The IBAN validator to use.</param>
		public FluentIbanValidator(IIbanValidator ibanValidator)
			: base(Resources.Not_a_valid_IBAN)
		{
			_ibanValidator = ibanValidator ?? throw new ArgumentNullException(nameof(ibanValidator));
		}

		/// <inheritdoc />
		protected override bool IsValid(PropertyValidatorContext context)
		{
			return context.PropertyValue == null || _ibanValidator.Validate((string)context.PropertyValue).IsValid;
		}
	}
}

using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;

namespace IbanNet.FluentValidation
{
	/// <summary>
	/// FluentValidation rule builder extensions.
	/// </summary>
	public static class RuleBuilderExtensions
	{
		/// <summary>
		/// Defines an IBAN validator on the current rule builder, but only for string properties.
		/// Validation will fail if the value returned by the lambda is not a valid international bank account number.
		/// </summary>
		/// <typeparam name="T">Type of object being validated</typeparam>
		/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
		/// <param name="ibanValidator">The <see cref="IIbanValidator"/> instance to use for validation.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> Iban<T>(
			this IRuleBuilder<T, string> ruleBuilder, IIbanValidator ibanValidator)
		{
			return ruleBuilder.Iban(new IbanValidationOptions { Validator = ibanValidator });
		}

		/// <summary>
		/// Defines an IBAN validator on the current rule builder, but only for string properties.
		/// Validation will fail if the value returned by the lambda is not a valid international bank account number.
		/// </summary>
		/// <typeparam name="T">Type of object being validated</typeparam>
		/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
		/// <param name="validationOptions">The <see cref="IbanValidationOptions"/> options to use for validation.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> Iban<T>(
			this IRuleBuilder<T, string> ruleBuilder, IbanValidationOptions validationOptions)
		{
			if (ruleBuilder == null)
			{
				throw new ArgumentNullException(nameof(ruleBuilder));
			}

			if (validationOptions == null)
			{
				throw new ArgumentNullException(nameof(validationOptions));
			}

			var countryCodes = new HashSet<string>(validationOptions.CountryCodes ?? new string[0]);
			return ruleBuilder
				.SetValidator(new FluentIbanCountryValidator(countryCodes))
				.SetDependentValidator(new FluentIbanValidator(validationOptions.Validator));
		}

		/// <summary>
		/// Sets a validator that only executes when the previous validator succeeds.
		/// </summary>
		private static IRuleBuilderOptions<T, string> SetDependentValidator<T>(this IRuleBuilderOptions<T, string> ruleBuilder, IPropertyValidator propertyValidator)
		{
			bool isInvalid = false;
			return ruleBuilder
				.OnFailure(obj => isInvalid = true)
				.SetValidator(propertyValidator)
				.When(_ => !isInvalid);
		}
	}
}
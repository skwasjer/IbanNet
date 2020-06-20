using System;
using FluentValidation;

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
			if (ruleBuilder is null)
			{
				throw new ArgumentNullException(nameof(ruleBuilder));
			}

			return ruleBuilder.SetValidator(new FluentIbanValidator(ibanValidator));
		}
	}
}

using System.Collections.Generic;
using IbanNet.Registry;
using IbanNet.Validation.Methods;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation
{
	/// <summary>
	/// Describes how validation rules are resolved by validation method.
	/// </summary>
	internal interface IValidationRuleResolver
	{
		/// <summary>
		/// Gets the rules for specified <paramref name="validationMethod"/> and <paramref name="registry"/>.
		/// </summary>
		/// <param name="validationMethod">The validation method for which the validation rules are requested.</param>
		/// <param name="registry">The IBAN registry.</param>
		/// <returns>An enumerable with the validation rules to be used for validation.</returns>
		IEnumerable<IIbanValidationRule> GetRules(ValidationMethod validationMethod, IIbanRegistry registry);
	}
}

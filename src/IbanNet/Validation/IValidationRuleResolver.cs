using System.Collections.Generic;
using IbanNet.Registry;
using IbanNet.Validation.Methods;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation
{
	/// <summary>
	/// Describes how validation rules are resolved by validation method.
	/// </summary>
	public interface IValidationRuleResolver
	{
		/// <summary>
		/// Gets the rules for specified <paramref name="validationMethod"/> and <paramref name="acceptedCountries"/>.
		/// </summary>
		/// <param name="validationMethod">The validation method for which the validation rules are requested.</param>
		/// <param name="acceptedCountries">The accepted IBAN countries.</param>
		/// <returns>An enumerable with the validation rules to be used for validation.</returns>
		IEnumerable<IIbanValidationRule> GetRules(ValidationMethod validationMethod, IDictionary<string, IbanCountry> acceptedCountries);
	}
}

using System.Collections.Generic;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation
{
	/// <summary>
	/// Describes how validation rules are resolved by validation method.
	/// </summary>
	internal interface IValidationRuleResolver
	{
		/// <summary>
		/// Gets the validation rules.
		/// </summary>
		/// <returns>An enumerable with the validation rules to be used for validation.</returns>
		IEnumerable<IIbanValidationRule> GetRules();
	}
}

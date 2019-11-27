using System.Collections.Generic;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation.Methods
{
	/// <summary>
	/// Fast validation consists of all built-in IBAN validation rules, except for checking the entire IBAN structure.
	/// This means that the IBAN could potentially contain certain characters that are officially not allowed, but could
	/// pass all other criteria (even including check digit).
	/// </summary>
	public class FastValidation : ValidationMethod
	{
		internal override IEnumerable<IIbanValidationRule> GetRules()
		{
			yield return new NotNullOrEmptyRule();
			yield return new NoIllegalCharactersRule();
			yield return new HasCountryCodeRule();
			yield return new HasIbanChecksumRule();
			yield return new IsValidCountryCodeRule();
			yield return new IsValidLengthRule();
			yield return new Mod97Rule();
		}
	}
}

using IbanNet.Extensions;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN has a country code but does not check the validity of the country code itself.
	/// </summary>
	internal class HasCountryCodeRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public ValidationRuleResult Validate(ValidationRuleContext context)
		{
			string iban = context.Value;
			// First 2 chars must be a-z or A-Z.
			if (iban.Length < 2 || !iban[0].IsAsciiLetter() || !iban[1].IsAsciiLetter())
			{
				return new IllegalCharactersResult();
			}

			return ValidationRuleResult.Success;
		}
	}
}

using IbanNet.Extensions;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	internal class HasCountryCodeRule : IIbanValidationRule
	{
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

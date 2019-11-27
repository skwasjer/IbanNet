using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	internal class HasIbanChecksumRule : IIbanValidationRule
	{
		public ValidationRuleResult Validate(ValidationRuleContext context, string iban)
		{
			if (iban.Length < 4
				// 00 and 01 are invalid.
			 || iban[2] == '0' && (iban[3] == '0' || iban[3] == '1')
				// 99 is invalid.
			 || iban[2] == '9' && iban[3] == '9')
			{
				return new IllegalCharactersResult();
			}

			return ValidationRuleResult.Success;
		}
	}
}

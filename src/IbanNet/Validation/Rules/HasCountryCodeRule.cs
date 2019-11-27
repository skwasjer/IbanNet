using IbanNet.Extensions;

namespace IbanNet.Validation.Rules
{
	internal class HasCountryCodeRule : IIbanValidationRule
	{
		public void Validate(ValidationContext context, string iban)
		{
			// First 2 chars must be a-z or A-Z.
			if (iban.Length < 2 || !iban[0].IsAsciiLetter() || !iban[1].IsAsciiLetter())
			{
				context.Result = IbanValidationResult.IllegalCharacters;
			}
		}
	}
}

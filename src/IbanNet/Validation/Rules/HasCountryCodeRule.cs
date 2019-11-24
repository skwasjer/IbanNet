using IbanNet.Extensions;

namespace IbanNet.Validation.Rules
{
	internal class HasCountryCodeRule : IIbanValidationRule
	{
		public void Validate(ValidationContext context)
		{
			// First 2 chars must be a-z or A-Z.
			if (context.Value.Length < 2 || !context.Value[0].IsAsciiLetter() || !context.Value[1].IsAsciiLetter())
			{
				context.Result = IbanValidationResult.IllegalCharacters;
			}
		}
	}
}

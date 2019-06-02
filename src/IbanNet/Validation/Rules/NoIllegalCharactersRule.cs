using IbanNet.Extensions;

namespace IbanNet.Validation.Rules
{
	internal class NoIllegalCharactersRule : IIbanValidationRule
	{
		public void Validate(ValidationRuleContext context, string iban)
		{
			// ReSharper disable once LoopCanBeConvertedToQuery : justification -> faster
			// ReSharper disable once ForCanBeConvertedToForeach : justification -> faster
			for (int i = 0; i < iban.Length; i++)
			{
				char c = iban[i];
				// All chars must be 0-9, a-z or A-Z.
				if (!(c.IsAsciiLetter() || c.IsAsciiDigit()))
				{
					context.Result = IbanValidationResult.IllegalCharacters;
					return;
				}
			}
		}
	}
}

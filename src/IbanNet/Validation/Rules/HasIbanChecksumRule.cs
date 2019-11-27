namespace IbanNet.Validation.Rules
{
	internal class HasIbanChecksumRule : IIbanValidationRule
	{
		public void Validate(ValidationContext context, string iban)
		{
			if (iban.Length < 4
				// 00 and 01 are invalid.
			 || iban[2] == '0' && (iban[3] == '0' || iban[3] == '1')
				// 99 is invalid.
			 || iban[2] == '9' && iban[3] == '9')
			{
				context.Result = IbanValidationResult.IllegalCharacters;
			}
		}
	}
}

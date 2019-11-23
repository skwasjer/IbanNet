namespace IbanNet.Validation.Rules
{
	internal class HasIbanChecksumRule : IIbanValidationRule
	{
		public void Validate(ValidationContext context)
		{
			if (context.Value.Length < 4
				// 00 and 01 are invalid.
			 || context.Value[2] == '0' && (context.Value[3] == '0' || context.Value[3] == '1')
				// 99 is invalid.
			 || context.Value[2] == '9' && context.Value[3] == '9')
			{
				context.Result = IbanValidationResult.IllegalCharacters;
			}
		}
	}
}

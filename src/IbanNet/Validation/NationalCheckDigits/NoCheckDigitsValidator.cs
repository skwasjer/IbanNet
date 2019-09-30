namespace IbanNet.Validation.NationalCheckDigits
{
	internal class NoCheckDigitsValidator
		: INationalCheckDigitsValidator
	{
		public bool Validate(string iban)
		{
			return true;
		}
	}
}
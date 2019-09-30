namespace IbanNet.Validation.NationalCheckDigits
{
	internal interface INationalCheckDigitsValidator
	{
		bool Validate(string iban);
	}
}
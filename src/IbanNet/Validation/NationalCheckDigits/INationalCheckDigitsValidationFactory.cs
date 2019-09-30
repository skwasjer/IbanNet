namespace IbanNet.Validation.NationalCheckDigits
{
	interface INationalCheckDigitsValidationFactory
	{
		INationalCheckDigitsValidator CreateValidator(string country);
	}
}

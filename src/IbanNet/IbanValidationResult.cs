namespace IbanNet
{
	public enum IbanValidationResult
	{
		Valid,
		IllegalCharacters,
		UnknownCountryCode,
		InvalidStructure,
		WrongCheckDigits,
		IncorrectLength,
	}
}
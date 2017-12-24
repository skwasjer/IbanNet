namespace IbanNet
{
	public enum IbanValidationResult
	{
		Invalid,
		Valid,
		IllegalCharacters,
		UnknownCountryCode,
		InvalidStructure,
		WrongCheckDigits,
		CantCheckLength,
		IncorrectLength,
	}
}
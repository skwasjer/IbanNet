using IbanNet.CheckDigits.Calculators;

namespace IbanNet.Validation.NationalCheckDigits
{
	internal class CinNationalCheckDigitsValidator : NationalCheckDigitsValidator
	{
		private const int CharCodeA = 'A';
		private const int CheckDigitCount = 1;

		public CinNationalCheckDigitsValidator() : base(new CinCheckDigitsCalculator(), "IT", "SM")
		{
		}

		protected override string GetCheckString(string bban)
		{
			return bban.Substring(CheckDigitCount);
		}

		protected override int GetExpectedCheckDigits(string bban)
		{
			return bban[0] - CharCodeA;
		}
	}
}
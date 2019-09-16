using IbanNet.CheckDigits.Calculators;

namespace IbanNet.Validation.NationalCheckDigits
{
	internal class Mod11ValidatorDigitsValidator : NationalCheckDigitsValidator
	{
		private const int CheckDigitCount = 1;

		public Mod11ValidatorDigitsValidator() : base(new Mod11CheckDigitsCalculator(), "NO")
		{
		}

		protected override string GetCheckString(string bban)
		{
			return bban.Substring(4, bban.Length - 5);
		}

		protected override int GetExpectedCheckDigits(string bban)
		{
			return int.Parse(bban.Substring(bban.Length - CheckDigitCount));
		}
	}
}
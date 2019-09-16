using IbanNet.CheckDigits.Calculators;

namespace IbanNet.Validation.NationalCheckDigits
{
	internal class Mod97NationalCheckDigitsValidator : NationalCheckDigitsValidator
	{
		public Mod97NationalCheckDigitsValidator() : base(new Mod97CheckDigitsCalculator(), "BA")
		{
		}

		protected override string GetCheckString(string bban)
		{
			return bban;
		}

		protected override int GetExpectedCheckDigits(string bban)
		{
			return 1;
		}
	}
}
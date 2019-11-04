using IbanNet.CheckDigits.Calculators;

namespace IbanNet.Validation.NationalCheckDigits
{
	internal class NorwayMod11ValidatorDigitsValidator : NationalCheckDigitsValidator
	{
		private const int CheckDigitCount = 1;

		public NorwayMod11ValidatorDigitsValidator() : base(new Mod11CheckDigitsCalculator(), "NO")
		{
		}

		public override bool Validate(string bban)
		{
			if (bban.Substring(4, 2) == "00")
			{
				return true;
			}
			return base.Validate(bban);
		}

		protected override string GetCheckString(string bban)
		{
			return bban.Substring(0, 10);
		}

		protected override int GetExpectedCheckDigits(string bban)
		{
			return int.Parse(bban.Substring(bban.Length - CheckDigitCount));
		}
	}
}

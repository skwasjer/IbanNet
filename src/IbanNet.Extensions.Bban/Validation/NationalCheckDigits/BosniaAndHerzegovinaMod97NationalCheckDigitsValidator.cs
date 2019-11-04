using IbanNet.CheckDigits.Calculators;

namespace IbanNet.Validation.NationalCheckDigits
{
	internal class BosniaAndHerzegovinaMod97NationalCheckDigitsValidator : NationalCheckDigitsValidator
	{
		public BosniaAndHerzegovinaMod97NationalCheckDigitsValidator() : base(new Mod97From98CheckDigitsCalculator(), "BA")
		{
		}

		protected override string GetCheckString(string bban)
		{
			return bban.Substring(0, bban.Length - 2) + "00";
		}

		protected override int GetExpectedCheckDigits(string bban)
		{
			return int.Parse(bban.Substring(bban.Length - 2));
		}
	}
}

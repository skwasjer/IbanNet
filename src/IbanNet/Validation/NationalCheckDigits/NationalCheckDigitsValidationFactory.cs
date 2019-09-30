using System.Linq;
using System.Runtime.InteropServices;

namespace IbanNet.Validation.NationalCheckDigits
{
	internal class NationalCheckDigitsValidationFactory : INationalCheckDigitsValidationFactory
	{
		private INationalCheckDigitsValidator NoCheckDigitsValidator { get; } = new NoCheckDigitsValidator();
		public INationalCheckDigitsValidator CreateValidator(string country)
		{
			switch (country)
			{
				case "FR":
					return new FrenchNationalCheckDigitsValidator();
				case "IT":
					return new ItalianNationalCheckDigitsValidator();
				default:
					return NoCheckDigitsValidator;
			}
		}
	}
}
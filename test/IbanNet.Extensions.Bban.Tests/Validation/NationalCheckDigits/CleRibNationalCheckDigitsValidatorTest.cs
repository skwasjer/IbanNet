using NUnit.Framework;

namespace IbanNet.Validation.NationalCheckDigits
{
	[TestFixture]
	public class CleRibNationalCheckDigitsValidatorTest
	{
		// FR, MR, MC
		[TestCase("30001007941234567890185")]
		[TestCase("30004000031234567890143")]
		[TestCase("30006000011234567890189")]
		public void Given_valid_bban_should_validate(string bban)
		{
			var validator = new CleRibNationalCheckDigitsValidator();
			Assert.That(validator.Validate(bban), Is.True);
		}

		// FR, MR, MC
		[TestCase("30001007941234567890186")]
		[TestCase("30004000031234567890144")]
		[TestCase("30006000011234567890190")]
		public void Given_invalid_bban_should_not_validate(string bban)
		{
			var validator = new CleRibNationalCheckDigitsValidator();
			Assert.That(validator.Validate(bban), Is.False);
		}
	}
}

using NUnit.Framework;

namespace IbanNet.Validation.NationalCheckDigits
{
	[TestFixture]
	public class BosniaAndHerzegovinaMod97NationalCheckDigitsValidatorTest
	{
		// BA
		[TestCase("1990440001200279")]
		[TestCase("1290079401028494")]
		[TestCase("0060000123456758")]
		[TestCase("0060000123458698")]
		public void Given_valid_bban_should_validate(string bban)
		{
			var validator = new BosniaAndHerzegovinaMod97NationalCheckDigitsValidator();
			Assert.That(validator.Validate(bban), Is.True);
		}

		// BA
		[TestCase("1990440001200278")]
		[TestCase("1290079401028493")]
		[TestCase("0060000123456757")]
		[TestCase("0060000123458697")]
		public void Given_invalid_bban_should_not_validate(string bban)
		{
			var validator = new BosniaAndHerzegovinaMod97NationalCheckDigitsValidator();
			Assert.That(validator.Validate(bban), Is.False);
		}
	}
}

using NUnit.Framework;

namespace IbanNet.Validation.NationalCheckDigits
{
	[TestFixture]
	public class CinNationalCheckDigitsValidatorTest
	{
        //IT, SM
		[TestCase("C0114962654315W0AV67Q9J")]
		[TestCase("I0900245288OKALSKMZQPVZ")]
		[TestCase("U9382566253RRP8KQG4ALJZ")]
		public void Given_valid_bban_should_validate(string bban)
		{
            var validator = new CinNationalCheckDigitsValidator();
            Assert.That(validator.Validate(bban), Is.True);
		}

		//IT, SM
		[TestCase("V0114962654315W0AV67Q9J")]
		[TestCase("B0900245288OKALSKMZQPVZ")]
		[TestCase("L9382566253RRP8KQG4ALJZ")]
		public void Given_invalid_bban_should_not_validate(string bban)
		{
			var validator = new CinNationalCheckDigitsValidator();
			Assert.That(validator.Validate(bban), Is.False);
		}
	}
}

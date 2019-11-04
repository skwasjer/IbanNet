using System;
using System.Collections.Generic;
using System.Text;
using IbanNet.Validation.NationalCheckDigits;
using NUnit.Framework;

namespace IbanNet.CheckDigits.Calculators
{
	[TestFixture]
	public class NorwayMod11ValidatorDigitsValidatorTest
	{
		// NO
        [TestCase("12340012345")]
        [TestCase("12340012346")]
        [TestCase("12340012347")]
		public void Given_a_double_zero_bban_should_validate(string bban)
        {
	        var validator = new NorwayMod11ValidatorDigitsValidator();
	        Assert.That(validator.Validate(bban), Is.True);
        }

        // NO
		[TestCase("86011117947")]
		[TestCase("02056439652")]
		[TestCase("12345678903")]
		public void Given_valid_bban_should_validate(string bban)
		{
			var validator = new NorwayMod11ValidatorDigitsValidator();
			Assert.That(validator.Validate(bban), Is.True);
		}

		// NO
		[TestCase("02056439653")]
		public void Given_invalid_bban_should_not_validate(string bban)
		{
			var validator = new NorwayMod11ValidatorDigitsValidator();
			Assert.That(validator.Validate(bban), Is.False);
		}
	}
}

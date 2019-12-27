using FluentAssertions;
using IbanNet.Validation.Results;
using NUnit.Framework;

namespace IbanNet.Validation.Rules
{
	[TestFixture]
	internal class HasIbanChecksumRuleTests
	{
		private readonly HasIbanChecksumRule _sut;

		public HasIbanChecksumRuleTests()
		{
			_sut = new HasIbanChecksumRule();
		}

		[TestCase("XX")]
		[TestCase("XX0")]
		[TestCase("XX00")]
		[TestCase("XX01")]
		[TestCase("XX99")]
		public void Given_invalid_checksum_when_validating_it_should_return_error(string value)
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

			actual.Should().BeOfType<IllegalCharactersResult>();
		}

		[Test]
		public void Given_valid_value_when_validating_it_should_return_success()
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext("XX45"));

			actual.Should().Be(ValidationRuleResult.Success);
		}
	}
}

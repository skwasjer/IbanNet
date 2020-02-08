using FluentAssertions;
using IbanNet.Validation.Results;
using Xunit;

namespace IbanNet.Validation.Rules
{
	public class HasIbanChecksumRuleTests
	{
		private readonly HasIbanChecksumRule _sut;

		public HasIbanChecksumRuleTests()
		{
			_sut = new HasIbanChecksumRule();
		}

		[Theory]
		[InlineData("XX")]
		[InlineData("XX0")]
		[InlineData("XX00")]
		[InlineData("XX01")]
		[InlineData("XX99")]
		public void Given_invalid_checksum_when_validating_it_should_return_error(string value)
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

			actual.Should().BeOfType<IllegalCharactersResult>();
		}

		[Fact]
		public void Given_valid_value_when_validating_it_should_return_success()
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext("XX45"));

			actual.Should().Be(ValidationRuleResult.Success);
		}
	}
}

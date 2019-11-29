using FluentAssertions;
using IbanNet.Validation.Results;
using NUnit.Framework;

namespace IbanNet.Validation.Rules
{
	[TestFixture]
	internal class NotNullOrEmptyRuleTests
	{
		private readonly NotEmptyRule _sut;

		public NotNullOrEmptyRuleTests()
		{
			_sut = new NotEmptyRule();
		}

		[Test]
		public void Given_empty_value_when_validating_it_should_return_error()
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(string.Empty, null));

			actual.Should().BeOfType<InvalidLengthResult>();
		}

		[Test]
		public void Given_non_empty_value_when_validating_it_should_return_success()
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext("not-empty", null));

			actual.Should().Be(ValidationRuleResult.Success);
		}
	}
}

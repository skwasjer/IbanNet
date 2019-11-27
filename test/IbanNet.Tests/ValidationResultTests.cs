using FluentAssertions;
using IbanNet.Validation.Results;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	internal class ValidationResultTests
	{
		[Test]
		public void Given_default_instance_when_getting_result_it_should_be_success()
		{
			var sut = new ValidationResult();

			sut.Result.Should().Be(ValidationRuleResult.Success);
			sut.IsValid.Should().BeTrue();
		}

		[Test]
		public void Given_result_is_success_when_getting_isValid_it_should_be_true()
		{
			var sut = new ValidationResult { Result = ValidationRuleResult.Success };

			sut.IsValid.Should().BeTrue();
		}

		[Test]
		public void Given_result_is_an_error_when_getting_isValid_it_should_be_false()
		{
			var sut = new ValidationResult { Result = new ErrorResult("Error") };

			sut.IsValid.Should().BeFalse();
		}
	}
}

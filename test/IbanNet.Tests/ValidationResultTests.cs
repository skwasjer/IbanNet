using FluentAssertions;
using IbanNet.Validation.Results;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	internal class ValidationResultTests
	{
		[Test]
		public void Given_result_is_success_when_getting_isValid_it_should_be_true()
		{
			var sut = new ValidationResult();

			sut.IsValid.Should().BeTrue();
		}

		[Test]
		public void Given_result_is_an_error_when_getting_isValid_it_should_be_false()
		{
			var sut = new ValidationResult { Error = new ErrorResult("Error") };

			sut.IsValid.Should().BeFalse();
			sut.Error.Should().NotBeNull();
		}
	}
}

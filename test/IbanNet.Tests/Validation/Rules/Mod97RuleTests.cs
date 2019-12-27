using System.Linq;
using FluentAssertions;
using IbanNet.CheckDigits.Calculators;
using IbanNet.Validation.Results;
using Moq;
using NUnit.Framework;

namespace IbanNet.Validation.Rules
{
	[TestFixture]
	internal class Mod97RuleTests
	{
		private Mod97Rule _sut;
		private Mock<ICheckDigitsCalculator> _calculatorMock;

		[SetUp]
		public void SetUp()
		{
			_calculatorMock = new Mock<ICheckDigitsCalculator>();
			_sut = new Mod97Rule(_calculatorMock.Object);
		}

		[Test]
		public void Given_invalid_value_when_validating_it_should_return_error()
		{
			const string value = "ABCD123456";
			const int invalidCheckDigit = 123;
			_calculatorMock
				.Setup(m => m.Compute(It.Is<char[]>(buf => buf.SequenceEqual("123456ABCD"))))
				.Returns(invalidCheckDigit)
				.Verifiable();

			// Act
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

			// Assert
			actual.Should().BeOfType<InvalidCheckDigitsResult>();
			_calculatorMock.Verify();
		}

		[Test]
		public void Given_valid_value_when_validating_it_should_return_success()
		{
			const string value = "ABCD123456";
			const int expectedCheckDigit = 1;
			_calculatorMock
				.Setup(m => m.Compute(It.Is<char[]>(buf => buf.SequenceEqual("123456ABCD"))))
				.Returns(expectedCheckDigit)
				.Verifiable();

			// Act
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

			// Assert
			actual.Should().Be(ValidationRuleResult.Success);
			_calculatorMock.Verify();
		}
	}
}

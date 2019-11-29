using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Results;
using Moq;
using NUnit.Framework;

namespace IbanNet.Validation.Rules
{
	[TestFixture]
	internal class IsMatchingStructureRuleTests
	{
		private IsMatchingStructureRule _sut;
		private Mock<IStructureValidator> _structureValidatorMock;
		private Mock<IStructureValidationFactory> _structureValidationFactoryMock;

		[SetUp]
		public void SetUp()
		{
			_structureValidatorMock = new Mock<IStructureValidator>();

			_structureValidationFactoryMock = new Mock<IStructureValidationFactory>();
			_structureValidationFactoryMock
				.Setup(m => m.CreateValidator(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(_structureValidatorMock.Object)
				.Verifiable();

			_sut = new IsMatchingStructureRule(_structureValidationFactoryMock.Object);
		}

		[Test]
		public void Given_no_country_when_validating_it_should_return_error()
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(string.Empty, null));

			// Assert
			actual.Should().BeOfType<InvalidStructureResult>();
			_structureValidationFactoryMock.Verify(m => m.CreateValidator(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void Given_valid_value_when_validating_it_should_return_success()
		{
			const string testValue = "XX";
			var country = new CountryInfo("NL")
			{
				Iban =
				{
					Structure = "my-structure"
				}
			};
			_structureValidatorMock
				.Setup(m => m.Validate(testValue))
				.Returns(true)
				.Verifiable();

			// Act
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(testValue, country));

			// Assert
			actual.Should().Be(ValidationRuleResult.Success);
			_structureValidatorMock.Verify();
			_structureValidationFactoryMock.Verify(m => m.CreateValidator(country.TwoLetterISORegionName, country.Iban.Structure), Times.Once);
		}

		[Test]
		public void Given_invalid_value_when_validating_it_should_return_error()
		{
			const string testValue = "XX";
			var country = new CountryInfo("NL")
			{
				Iban =
				{
					Structure = "my-structure"
				}
			};
			_structureValidatorMock
				.Setup(m => m.Validate(testValue))
				.Returns(false)
				.Verifiable();

			// Act
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(testValue, country));

			// Assert
			actual.Should().BeOfType<InvalidStructureResult>();
			_structureValidatorMock.Verify();
			_structureValidationFactoryMock.Verify(m => m.CreateValidator(country.TwoLetterISORegionName, country.Iban.Structure), Times.Once);
		}
	}
}

using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Results;
using Moq;
using Xunit;

namespace IbanNet.Validation.Rules
{
	public class IsMatchingStructureRuleTests
	{
		private readonly IsMatchingStructureRule _sut;
		private readonly Mock<IStructureValidator> _structureValidatorMock;
		private readonly Mock<IStructureValidationFactory> _structureValidationFactoryMock;

		public IsMatchingStructureRuleTests()
		{
			_structureValidatorMock = new Mock<IStructureValidator>();

			_structureValidationFactoryMock = new Mock<IStructureValidationFactory>();
			_structureValidationFactoryMock
				.Setup(m => m.CreateValidator(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(_structureValidatorMock.Object)
				.Verifiable();

			_sut = new IsMatchingStructureRule(_structureValidationFactoryMock.Object);
		}

		[Fact]
		public void Given_no_country_when_validating_it_should_return_error()
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(string.Empty));

			// Assert
			actual.Should().BeOfType<InvalidStructureResult>();
			_structureValidationFactoryMock.Verify(m => m.CreateValidator(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
		}

		[Fact]
		public void Given_valid_value_when_validating_it_should_return_success()
		{
			const string testValue = "XX";
			var country = new IbanCountry("NL")
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
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(testValue)
			{
				Country = country
			});

			// Assert
			actual.Should().Be(ValidationRuleResult.Success);
			_structureValidatorMock.Verify();
			_structureValidationFactoryMock.Verify(m => m.CreateValidator(country.TwoLetterISORegionName, country.Iban.Structure), Times.Once);
		}

		[Fact]
		public void Given_invalid_value_when_validating_it_should_return_error()
		{
			const string testValue = "XX";
			var country = new IbanCountry("NL")
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
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(testValue)
			{
				Country = country
			});

			// Assert
			actual.Should().BeOfType<InvalidStructureResult>();
			_structureValidatorMock.Verify();
			_structureValidationFactoryMock.Verify(m => m.CreateValidator(country.TwoLetterISORegionName, country.Iban.Structure), Times.Once);
		}
	}
}

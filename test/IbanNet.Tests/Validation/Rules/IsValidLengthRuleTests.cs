using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Results;
using NUnit.Framework;

namespace IbanNet.Validation.Rules
{
	[TestFixture]
	internal class IsValidLengthRuleTests
	{
		private readonly IsValidLengthRule _sut;

		public IsValidLengthRuleTests()
		{
			_sut = new IsValidLengthRule();
		}

		[TestCase(9)]
		[TestCase(11)]
		public void Given_value_of_invalid_length_when_validating_it_should_return_error(int count)
		{
			string value = new string('0', count);
			var context = new ValidationRuleContext(value)
			{
				Country = new IbanCountry("XX")
				{
					Iban =
					{
						Length = 10
					}
				}
			};

			// Act
			ValidationRuleResult actual = _sut.Validate(context);

			// Assert
			actual.Should().BeOfType<InvalidLengthResult>();
		}

		[Test]
		public void Given_value_of_valid_length_when_validating_it_should_return_success()
		{
			string value = new string('0', 10);
			var context = new ValidationRuleContext(value)
			{
				Country = new IbanCountry("XX")
				{
					Iban =
					{
						Length = 10
					}
				}
			};

			// Act
			ValidationRuleResult actual = _sut.Validate(context);

			// Assert
			actual.Should().Be(ValidationRuleResult.Success);
		}
	}
}

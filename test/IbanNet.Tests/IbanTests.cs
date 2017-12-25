using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace IbanNet.Tests
{
	[TestFixture]
	internal class IbanTests
	{
		private Mock<IIbanValidator> _ibanValidatorMock;

		private const string ValidIban = "AD1200012030200359100100";
		private const string ValidIbanPartitioned = "AD12 0001 2030 2003 5910 0100";
		private const string InvalidIban = "__INVALID_IBAN";

		[SetUp]
		public virtual void SetUp()
		{
			_ibanValidatorMock = new Mock<IIbanValidator>();
			_ibanValidatorMock
				.Setup(m => m.Validate(ValidIban))
				.Returns(IbanValidationResult.Valid);
			_ibanValidatorMock
				.Setup(m => m.Validate(InvalidIban))
				.Returns(IbanValidationResult.IllegalCharacters);

			Iban.Validator = _ibanValidatorMock.Object;
		}

		public class When_parsing_iban : IbanTests
		{
			[Test]
			public void With_null_value_should_throw()
			{
				// Act
				Action act = () => Iban.Parse(null);

				// Assert
				act.ShouldThrow<ArgumentNullException>("the provided value was null").Which.ParamName.Should().Be("value");
			}

			[Test]
			public void With_invalid_value_should_throw()
			{
				// Act
				Action act = () => Iban.Parse(InvalidIban);

				// Assert
				act.ShouldThrow<IbanFormatException>("the provided value was invalid")
					.Which.Result.Should().Be(IbanValidationResult.IllegalCharacters);
			}

			[Test]
			public void With_valid_value_should_return_iban()
			{
				Iban iban = null;

				// Act
				Action act = () => iban = Iban.Parse(ValidIban);

				// Assert
				act.ShouldNotThrow<IbanFormatException>();
				iban.Should().NotBeNull("the value should be parsed")
					.And.Subject.As<Iban>().ToString(Iban.Formats.Flat).Should().Be(ValidIban, "the returned value should match the provided value");
			}
		}

		public class When_trying_to_parse_iban : IbanTests
		{
			[Test]
			public void With_null_value_should_return_false()
			{
				Iban iban;

				// Act
				var actual = Iban.TryParse(null, out iban);

				// Assert
				actual.Should().BeFalse("the provided value was null which is not valid");
				iban.Should().BeNull("parsing did not succeed");

			}

			[Test]
			public void With_invalid_value_should_return_false()
			{
				Iban iban;

				// Act
				var actual = Iban.TryParse(InvalidIban, out iban);

				// Assert
				actual.Should().BeFalse("the provided value was invalid");
				iban.Should().BeNull("parsing did not succeed");

				_ibanValidatorMock.Verify(m => m.Validate(It.IsAny<string>()), Times.Once);
			}

			[Test]
			public void With_valid_value_should_pass()
			{
				Iban iban;

				// Act
				var actual = Iban.TryParse(ValidIban, out iban);

				// Assert
				actual.Should().BeTrue("the provided value was valid");
				iban.Should().NotBeNull().And.Subject.As<Iban>().ToString(Iban.Formats.Flat).Should().Be(ValidIban);

				_ibanValidatorMock.Verify(m => m.Validate(It.IsAny<string>()), Times.Once);
			}
		}

		public class When_formatting : IbanTests
		{
			private Iban _iban;

			public override void SetUp()
			{
				base.SetUp();

				_iban = Iban.Parse(ValidIban);
			}

			[Test]
			public void With_null_format_should_throw()
			{
				// Act
				Action act = () => _iban.ToString(null);

				// Assert
				act.ShouldThrow<ArgumentNullException>("the provided format was null")
					.Which.ParamName.Should().Be("format");
			}

			[TestCase("f")]
			[TestCase("s")]
			[TestCase("invalid_format")]
			[TestCase("")]
			[TestCase(null)]
			public void With_invalid_format_should_throw(string format)
			{
				// Act
				Action act = () => _iban.ToString(format);

				// Assert
				act.ShouldThrow<ArgumentException>("the provided format was invalid")
					.Which.ParamName.Should().Be("format");
			}

			[TestCase(Iban.Formats.Flat, ValidIban)]
			[TestCase(Iban.Formats.Partitioned, ValidIbanPartitioned)]
			public void With_valid_format_should_succeed(string format, string expected)
			{
				// Act
				var actual = _iban.ToString(format);

				// Assert
				actual.Should().Be(expected);
			}

			[Test]
			public void With_default_format_should_return_partitioned()
			{
				// Act
				var actual = _iban.ToString();

				// Assert
				actual.Should().Be(ValidIbanPartitioned);
			}
		}
	}
}

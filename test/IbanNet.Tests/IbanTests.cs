using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	internal class IbanTests
	{
		private Mock<IIbanValidator> _ibanValidatorMock;

		[SetUp]
		public virtual void SetUp()
		{
			_ibanValidatorMock = new Mock<IIbanValidator>();
			_ibanValidatorMock
				.Setup(m => m.Validate(TestValues.ValidIban))
				.Returns(IbanValidationResult.Valid);
			_ibanValidatorMock
				.Setup(m => m.Validate(TestValues.InvalidIban))
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
				act.Should().Throw<ArgumentNullException>("the provided value was null").Which.ParamName.Should().Be("value");
			}

			[Test]
			public void With_invalid_value_should_throw()
			{
				// Act
				Action act = () => Iban.Parse(TestValues.InvalidIban);

				// Assert
				act.Should().Throw<IbanFormatException>("the provided value was invalid")
					.Which.Result
					.Should().Be(IbanValidationResult.IllegalCharacters);
			}

			[Test]
			public void With_valid_value_should_return_iban()
			{
				Iban iban = null;

				// Act
				Action act = () => iban = Iban.Parse(TestValues.ValidIban);

				// Assert
				act.Should().NotThrow<IbanFormatException>();
				iban.Should().NotBeNull("the value should be parsed")
					.And.BeOfType<Iban>()
					.Which.ToString()
					.Should().Be(TestValues.ValidIban, "the returned value should match the provided value");
			}
		}

		public class When_trying_to_parse_iban : IbanTests
		{
			[Test]
			public void With_null_value_should_return_false()
			{
				// Act
				var actual = Iban.TryParse(null, out var iban);

				// Assert
				actual.Should().BeFalse("the provided value was null which is not valid");
				iban.Should().BeNull("parsing did not succeed");

			}

			[Test]
			public void With_invalid_value_should_return_false()
			{
				// Act
				var actual = Iban.TryParse(TestValues.InvalidIban, out var iban);

				// Assert
				actual.Should().BeFalse("the provided value was invalid");
				iban.Should().BeNull("parsing did not succeed");

				_ibanValidatorMock.Verify(m => m.Validate(It.IsAny<string>()), Times.Once);
			}

			[Test]
			public void With_valid_value_should_pass()
			{
				// Act
				var actual = Iban.TryParse(TestValues.ValidIban, out var iban);

				// Assert
				actual.Should().BeTrue("the provided value was valid");
				iban.Should().NotBeNull().
					And.BeOfType<Iban>()
					.Which.ToString()
					.Should().Be(TestValues.ValidIban);

				_ibanValidatorMock.Verify(m => m.Validate(It.IsAny<string>()), Times.Once);
			}
		}

		public class When_formatting : IbanTests
		{
			private Iban _iban;

			public override void SetUp()
			{
				base.SetUp();

				_iban = Iban.Parse(TestValues.ValidIban);
			}

			[Test]
			public void With_null_format_should_throw()
			{
				// Act
				Action act = () => _iban.ToString(null);

				// Assert
				act.Should().Throw<ArgumentNullException>("the provided format was null")
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
				act.Should().Throw<ArgumentException>("the provided format was invalid")
					.Which.ParamName.Should().Be("format");
			}

			[TestCase(Iban.Formats.Flat, TestValues.ValidIban)]
			[TestCase(Iban.Formats.Partitioned, TestValues.ValidIbanPartitioned)]
			public void With_valid_format_should_succeed(string format, string expected)
			{
				// Act
				var actual = _iban.ToString(format);

				// Assert
				actual.Should().Be(expected);
			}

			[Test]
			public void With_default_format_should_return_flat()
			{
				// Act
				var actual = _iban.ToString();

				// Assert
				actual.Should().Be(TestValues.ValidIban);
			}
		}
	}
}

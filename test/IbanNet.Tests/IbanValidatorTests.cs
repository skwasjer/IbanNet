using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Results;
using IbanNet.Validation.Rules;
using Moq;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	internal class IbanValidatorTests
	{
		public class Given_invalid_options : IbanValidatorTests
		{
			[TestCaseSource(nameof(CtorWithOptionsTestCases))]
			public void When_creating_instance_it_should_throw(IbanValidatorOptions options, Type expectedExceptionType)
			{
				// Act
				// ReSharper disable once ObjectCreationAsStatement
				Action act = () => new IbanValidator(options);

				// Assert
				act.Should()
					.Throw<ArgumentException>()
					.Which.Should()
					.BeOfType(expectedExceptionType);
			}


			public static IEnumerable CtorWithOptionsTestCases()
			{
				yield return new TestCaseData(null, typeof(ArgumentNullException));
				yield return new TestCaseData(new IbanValidatorOptions { Registry = null }, typeof(ArgumentException));
				yield return new TestCaseData(new IbanValidatorOptions { ValidationMethod = null }, typeof(ArgumentException));
			}
		}

		public class Given_default_supported_countries : IbanValidatorTests
		{
			[Test]
			public void When_getting_it_should_match_default_registry()
			{
				// Act
				IEnumerable<IbanCountry> actual = new IbanValidator().SupportedCountries.Values;

				// Assert
				actual.Should().BeEquivalentTo(IbanRegistry.Default);
			}

			[Test]
			public void When_casting_readonly_countries_to_dictionary_it_should_not_be_able_to_add()
			{
				var sut = new IbanValidator();
				var countries = (IDictionary<string, IbanCountry>)sut.SupportedCountries;

				// Act
				Action act = () => countries.Add("key", new IbanCountry("ZZ"));

				// Assert
				act.Should()
					.Throw<NotSupportedException>()
					.WithMessage("Collection is read-only.");
			}
		}

		public class Given_validator : IbanValidatorTests
		{
			private IbanValidator _sut;

			[SetUp]
			public void SetUp()
			{
				_sut = new IbanValidator();
			}

			[Test]
			public void When_validating_multiple_times_it_should_succeed()
			{
				const string iban = "NL91ABNA0417164300";

				// Act
				ValidationResult result1 = _sut.Validate(iban);
				ValidationResult result2 = _sut.Validate(iban);

				// Assert
				result1.IsValid.Should().BeTrue();
				result1.Should().BeEquivalentTo(result2);
			}
		}

		public class Given_options : IbanValidatorTests
		{
			[Test]
			public void It_should_set_property()
			{
				var opts = new IbanValidatorOptions();

				// Act
				var validator = new IbanValidator(opts);

				// Assert
				validator.Options.Should().BeSameAs(opts);
			}
		}

		public class Given_custom_rule_is_added : IbanValidatorTests
		{
			private IbanValidator _sut;
			private Mock<IIbanValidationRule> _customValidationRuleMock;

			[SetUp]
			public void SetUp()
			{
				_customValidationRuleMock = new Mock<IIbanValidationRule>();
				_customValidationRuleMock
					.Setup(m => m.Validate(It.IsAny<ValidationRuleContext>()))
					.Returns(ValidationRuleResult.Success);

				_sut = new IbanValidator(new IbanValidatorOptions
				{
					Rules = { _customValidationRuleMock.Object }
				});
			}

			[Test]
			public void When_validating_should_call_custom_rule()
			{
				const string iban = "NL91ABNA0417164300";

				// Act
				_sut.Validate(iban);

				// Assert
				_customValidationRuleMock.Verify(m => m.Validate(It.Is<ValidationRuleContext>(ctx => ctx.Value == iban)), Times.Once);
			}

			[Test]
			public void Given_custom_rule_throws_when_validating_should_rethrow()
			{
				const string iban = "NL91ABNA0417164300";
				Exception exception = new InvalidOperationException("My custom error");

				_customValidationRuleMock
					.Setup(m => m.Validate(It.IsAny<ValidationRuleContext>()))
					.Throws(exception);

				// Act
				Action act = () => _sut.Validate(iban);

				// Assert
				act.Should()
					.Throw<InvalidOperationException>()
					.Which.Should()
					.Be(exception);
			}

			[Test]
			public void Given_custom_rule_fails_when_validating_should_not_validate()
			{
				const string iban = "NL91ABNA0417164300";
				const string errorMessage = "My custom error";

				_customValidationRuleMock
					.Setup(m => m.Validate(It.IsAny<ValidationRuleContext>()))
					.Returns(new ErrorResult(errorMessage));

				// Act
				ValidationResult actual = _sut.Validate(iban);

				// Assert
				actual.Should()
					.BeEquivalentTo(new ValidationResult
					{
						AttemptedValue = iban,
						Error = new ErrorResult(errorMessage),
						Country = _sut.SupportedCountries["NL"]
					});
			}
		}
	}
}

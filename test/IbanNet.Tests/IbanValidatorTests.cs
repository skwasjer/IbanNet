using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation;
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
			public void When_creating_instance_it_should_throw(Action act, Type expectedExceptionType, string expectedParamName)
			{
				// Assert
				act.Should()
					.Throw<ArgumentException>()
					.Where(ex => ex.ParamName == expectedParamName)
					.Which.Should()
					.BeOfType(expectedExceptionType);
			}

			public static IEnumerable CtorWithOptionsTestCases()
			{
				// ReSharper disable ObjectCreationAsStatement
				yield return new TestCaseData((Action)(() => new IbanValidator(null)), typeof(ArgumentNullException), "options");
				yield return new TestCaseData((Action)(() => new IbanValidator(new IbanValidatorOptions { Registry = null })), typeof(ArgumentException), "options");
				yield return new TestCaseData((Action)(() => new IbanValidator(new IbanValidatorOptions { ValidationMethod = null })), typeof(ArgumentException), "options");
				yield return new TestCaseData((Action)(() => new IbanValidator(new IbanValidatorOptions(), null)), typeof(ArgumentNullException), "validationRuleResolver");
				yield return new TestCaseData((Action)(() => new IbanValidator(null, Mock.Of<IValidationRuleResolver>())), typeof(ArgumentNullException), "options");
				yield return new TestCaseData((Action)(() => new IbanValidator(new IbanValidatorOptions { Registry = null }, Mock.Of<IValidationRuleResolver>())), typeof(ArgumentException), "options");
				yield return new TestCaseData((Action)(() => new IbanValidator(new IbanValidatorOptions { ValidationMethod = null }, Mock.Of<IValidationRuleResolver>())), typeof(ArgumentException), "options");
				// ReSharper restore ObjectCreationAsStatement
			}
		}

		public class Given_default_supported_countries : IbanValidatorTests
		{
			[Test]
			public void When_getting_it_should_match_default_registry()
			{
				// Act
				IEnumerable<IbanCountry> actual = new IbanValidator().SupportedCountries;

				// Assert
				actual.Should().BeEquivalentTo(IbanRegistry.Default);
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

		public class Given_multiple_providers : IbanValidatorTests
		{
			private IbanValidator _sut;
			private Mock<IStructureValidationFactory>[] _structureFactoryMocks;

			[SetUp]
			public void SetUp()
			{
				var structureValidatorMock = new Mock<IStructureValidator>();
				structureValidatorMock.Setup(m => m.Validate(It.IsAny<string>())).Returns(true);

				_structureFactoryMocks = new[] {
					new Mock<IStructureValidationFactory>(),
					new Mock<IStructureValidationFactory>(),
					new Mock<IStructureValidationFactory>()
				};
				foreach (var mock in _structureFactoryMocks)
				{
					mock
						.Setup(m => m.CreateValidator(It.IsAny<string>(), It.IsAny<string>()))
						.Returns(structureValidatorMock.Object);
				}

				_sut = new IbanValidator(new IbanValidatorOptions
				{
					Registry = new IbanRegistry
					{
						Providers =
						{
							new IbanRegistryListProvider(
								new []
								{
									new IbanCountry("NL")
									{
										Iban =
										{
											Length = 18,
											Structure = "structure1",
											ValidationFactory = _structureFactoryMocks[0].Object
										}
									}
								}
							),
							new IbanRegistryListProvider(
								new []
								{
									new IbanCountry("NL")
									{
										Iban =
										{
											Length = 18,
											Structure = "structure2",
											ValidationFactory = _structureFactoryMocks[1].Object
										}
									}
								}
							),
							new IbanRegistryListProvider(
								new []
								{
									new IbanCountry("GB")
									{
										Iban =
										{
											Length = 22,
											Structure = "structure3",
											ValidationFactory = _structureFactoryMocks[2].Object
										}
									}
								}
							)
						}
					}
				});
			}

			[TestCase("NL91ABNA0417164300", "structure1", 0)]
			[TestCase("GB29NWBK60161331926819", "structure3", 2)]
			public void When_validating_it_should_use_structure_validator_of_first_provider_that_supports_the_country_code(string iban, string expectedStructure, int expectedMockCalled)
			{
				string expectedCountryCode = iban.Substring(0, 2);

				ValidationResult actual = _sut.Validate(iban);

				actual.IsValid.Should().BeTrue();
				for (int i = 0; i < _structureFactoryMocks.Length; i++)
				{
					_structureFactoryMocks[i]
						.Verify(m => m.CreateValidator(
							expectedCountryCode,
							expectedStructure),
							i == expectedMockCalled ? Times.Once() : Times.Never()
						);
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation;
using IbanNet.Validation.Results;
using IbanNet.Validation.Rules;
using Moq;
using Xunit;

namespace IbanNet
{
	public class IbanValidatorTests
	{
		public class Given_invalid_options : IbanValidatorTests
		{
			[Theory]
			[MemberData(nameof(CtorWithOptionsTestCases))]
			public void When_creating_instance_it_should_throw(Action act, Type expectedExceptionType, string expectedParamName)
			{
				// Assert
				act.Should()
					.Throw<ArgumentException>()
					.Where(ex => ex.ParamName == expectedParamName)
					.Which.Should()
					.BeOfType(expectedExceptionType);
			}

			public static IEnumerable<object[]> CtorWithOptionsTestCases()
			{
				// ReSharper disable ObjectCreationAsStatement
				yield return new object[] { (Action)(() => new IbanValidator(null)), typeof(ArgumentNullException), "options" };
				yield return new object[] { (Action)(() => new IbanValidator(new IbanValidatorOptions { Registry = null })), typeof(ArgumentException), "options" };
				yield return new object[] { (Action)(() => new IbanValidator(new IbanValidatorOptions(), null)), typeof(ArgumentNullException), "validationRuleResolver" };
				yield return new object[] { (Action)(() => new IbanValidator(null, Mock.Of<IValidationRuleResolver>())), typeof(ArgumentNullException), "options" };
				yield return new object[] { (Action)(() => new IbanValidator(new IbanValidatorOptions { Registry = null }, Mock.Of<IValidationRuleResolver>())), typeof(ArgumentException), "options" };
				// ReSharper restore ObjectCreationAsStatement
			}
		}

		public class Given_default_supported_countries : IbanValidatorTests
		{
			[Fact]
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
			private readonly IbanValidator _sut;

			public Given_validator()
			{
				_sut = new IbanValidator();
			}

			[Fact]
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
			[Fact]
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
			private readonly IbanValidator _sut;
			private readonly Mock<IIbanValidationRule> _customValidationRuleMock;

			public Given_custom_rule_is_added()
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

			[Fact]
			public void When_validating_should_call_custom_rule()
			{
				const string iban = "NL91ABNA0417164300";

				// Act
				_sut.Validate(iban);

				// Assert
				_customValidationRuleMock.Verify(m => m.Validate(It.Is<ValidationRuleContext>(ctx => ctx.Value == iban)), Times.Once);
			}

			[Fact]
			public void Given_custom_rule_throws_when_validating_should_wrap_as_exceptionResult()
			{
				const string iban = "NL91ABNA0417164300";
				Exception exception = new InvalidOperationException("My custom error");

				_customValidationRuleMock
					.Setup(m => m.Validate(It.IsAny<ValidationRuleContext>()))
					.Throws(exception);

				// Act
				Func<ValidationResult> act = () => _sut.Validate(iban);

				// Assert
				act.Should()
					.NotThrow()
					.Which.Error.Should()
					.BeOfType<ExceptionResult>()
					.Which.Exception.Should()
					.Be(exception);
			}

			[Fact]
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

		public class Given_validator_is_called_multiple_times : IbanValidatorTests
		{
			private readonly IbanValidator _sut;
			private readonly Mock<IStructureValidationFactory> _structureFactoryMock;

			public Given_validator_is_called_multiple_times()
			{
				var structureValidatorMock = new Mock<IStructureValidator>();
				structureValidatorMock.Setup(m => m.Validate(It.IsAny<string>())).Returns(true);

				_structureFactoryMock = new Mock<IStructureValidationFactory>();
				_structureFactoryMock
					.Setup(m => m.CreateValidator(It.IsAny<string>(), It.IsAny<string>()))
					.Returns(structureValidatorMock.Object);

				_sut = new IbanValidator(new IbanValidatorOptions
				{
					Registry = new IbanRegistry
					{
						Providers =
						{
							new IbanRegistryListProvider(
								new[]
								{
									new IbanCountry("NL")
									{
										Iban =
										{
											Length = 18,
											Structure = "structure1",
											ValidationFactory = _structureFactoryMock.Object
										}
									}
								}
							)
						}
					}
				});
			}

			[Fact]
			public void It_should_call_factory_once()
			{
				const string iban = "NL91ABNA0417164300";
				string expectedCountryCode = iban.Substring(0, 2);

				// Act
				for (int i = 0; i < 3; i++)
				{
					ValidationResult actual = _sut.Validate(iban);
					actual.IsValid.Should().BeTrue();
				}

				// Assert
				_structureFactoryMock.Verify(m => m.CreateValidator(
						expectedCountryCode,
						"structure1"),
					Times.Once
				);
			}
		}

		public class Given_multiple_providers : IbanValidatorTests
		{
			private readonly IbanValidator _sut;
			private readonly Mock<IStructureValidationFactory>[] _structureFactoryMocks;

			public Given_multiple_providers()
			{
				var structureValidatorMock = new Mock<IStructureValidator>();
				structureValidatorMock.Setup(m => m.Validate(It.IsAny<string>())).Returns(true);

				_structureFactoryMocks = new[] {
					new Mock<IStructureValidationFactory>(),
					new Mock<IStructureValidationFactory>(),
					new Mock<IStructureValidationFactory>()
				};
				foreach (Mock<IStructureValidationFactory> mock in _structureFactoryMocks)
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

			[Theory]
			[InlineData("NL91ABNA0417164300", "structure1", 0)]
			[InlineData("GB29NWBK60161331926819", "structure3", 2)]
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

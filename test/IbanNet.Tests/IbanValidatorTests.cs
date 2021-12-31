using IbanNet.Registry;
using IbanNet.Registry.Swift;
using IbanNet.Registry.Wikipedia;
using IbanNet.Validation;
using IbanNet.Validation.Results;
using IbanNet.Validation.Rules;

namespace IbanNet
{
    public class IbanValidatorTests
    {
        public class Given_invalid_options : IbanValidatorTests
        {
            [Theory]
            [MemberData(nameof(CtorWithOptionsTestCases))]
            public void When_creating_instance_it_should_throw(Func<IbanValidator> act, Type expectedExceptionType, string expectedParamName)
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
                yield return new object[] { (Func<IbanValidator>)(() => new IbanValidator(null)), typeof(ArgumentNullException), "options" };
                yield return new object[] { (Func<IbanValidator>)(() => new IbanValidator(new IbanValidatorOptions { Registry = null })), typeof(ArgumentException), "options" };
                yield return new object[] { (Func<IbanValidator>)(() => new IbanValidator(new IbanValidatorOptions(), null)), typeof(ArgumentNullException), "validationRuleResolver" };
                yield return new object[] { (Func<IbanValidator>)(() => new IbanValidator(null, Mock.Of<IValidationRuleResolver>())), typeof(ArgumentNullException), "options" };
                yield return new object[] { (Func<IbanValidator>)(() => new IbanValidator(new IbanValidatorOptions { Registry = null }, Mock.Of<IValidationRuleResolver>())), typeof(ArgumentException), "options" };
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
                actual.Should().BeEquivalentTo(IbanRegistry.Default, opts => opts.WithStrictOrdering());
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

        public class Given_multiple_providers : IbanValidatorTests
        {
            private readonly IbanValidator _sut;

            private readonly IbanCountry _correctNlCountry = new("NL") { Iban = new IbanStructure(new IbanSwiftPattern("NL2!n4!a10!n")) };
            private readonly IbanCountry _ignoredNlCountry = new("NL") { Iban = new IbanStructure(new IbanWikipediaPattern("50a")) };
            private readonly IbanCountry _correctGbCountry = new("GB") { Iban = new IbanStructure(new IbanWikipediaPattern("4a,14n")) };

            private readonly List<IbanCountry> _countries;

            public Given_multiple_providers()
            {
                _countries = new List<IbanCountry>
                {
                    _correctNlCountry,
                    _ignoredNlCountry,
                    _correctGbCountry
                };

                _sut = new IbanValidator(new IbanValidatorOptions
                {
                    Registry = new IbanRegistry
                    {
                        Providers =
                        {
                            new IbanRegistryListProvider(
                                new[]
                                {
                                    _correctNlCountry
                                }
                            ),
                            new IbanRegistryListProvider(
                                new[]
                                {
                                    _ignoredNlCountry,
                                    _correctGbCountry
                                }
                            )
                        }
                    }
                });
            }

            [Theory]
            [InlineData("NL91ABNA0417164300", 0)]
            [InlineData("GB29NWBK60161331926819", 2)]
            public void When_validating_it_should_use_structure_validator_of_first_provider_that_supports_the_country_code(string iban, int expectedCountryIndex)
            {
                ValidationResult actual = _sut.Validate(iban);

                actual.IsValid.Should().BeTrue();
                actual.Country.Should().BeSameAs(_countries[expectedCountryIndex]);
            }
        }
    }
}

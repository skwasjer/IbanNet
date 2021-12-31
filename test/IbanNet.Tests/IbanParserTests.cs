using IbanNet.Registry;
using IbanNet.Validation.Results;
using TestHelpers;

namespace IbanNet
{
    public class IbanParserTests
    {
        private readonly IbanValidatorStub _ibanValidatorStub;
        private readonly IbanParser _sut;

        protected IbanParserTests()
        {
            _ibanValidatorStub = new IbanValidatorStub();
            _sut = new IbanParser(_ibanValidatorStub);
        }

        public class Integration
        {
            private readonly IbanParser _sut;

            public Integration()
            {
                _sut = new IbanParser(new IbanValidator());
            }

            [Theory]
            [InlineData(null, typeof(ArgumentNullException))]
            [InlineData("", typeof(IbanFormatException))]
            [InlineData("AD12000120359100100", typeof(IbanFormatException))]
            [InlineData("Invalid", typeof(IbanFormatException))]
            public void Given_invalid_value_when_parsing_it_should_throw(string attemptedIbanValue, Type expectedExceptionType)
            {
                // Act
                Action act = () => _sut.Parse(attemptedIbanValue);

                // Assert
                act.Should()
                    .Throw<Exception>()
                    .Which.Should()
                    .BeOfType(expectedExceptionType);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("AD12000120359100100")]
            [InlineData("Invalid")]
            public void Given_invalid_value_when_trying_parsing_it_should_not_throw_and_return_false(string attemptedIbanValue)
            {
                // Act
                Func<bool> act = () => _sut.TryParse(attemptedIbanValue, out _);

                // Assert
                act.Should().NotThrow().Which.Should().BeFalse();
            }
        }

        public class When_creating_instance
        {
            [Fact]
            public void With_null_validator_it_should_throw()
            {
                IIbanValidator ibanValidator = null;

                // Act
                // ReSharper disable once AssignNullToNotNullAttribute
                Func<IbanParser> parser = () => new IbanParser(ibanValidator);

                // Assert
                parser.Should()
                    .ThrowExactly<ArgumentNullException>()
                    .Which.ParamName.Should()
                    .Be(nameof(ibanValidator));
            }

            [Fact]
            public void With_null_registry_it_should_throw()
            {
                IIbanRegistry registry = null;

                // Act
                // ReSharper disable once AssignNullToNotNullAttribute
                Func<IbanParser> parser = () => new IbanParser(registry);

                // Assert
                parser.Should()
                    .ThrowExactly<ArgumentNullException>()
                    .Which.ParamName.Should()
                    .Be(nameof(registry));
            }
        }

        public class When_parsing_iban : IbanParserTests
        {
            [Fact]
            public void With_null_value_should_throw()
            {
                string value = null;

                // Act
                // ReSharper disable once AssignNullToNotNullAttribute
                Action act = () => _sut.Parse(value);

                // Assert
                act.Should()
                    .Throw<ArgumentNullException>("the provided value was null")
                    .Which.ParamName.Should()
                    .Be(nameof(value));
            }

            [Fact]
            public void With_invalid_value_should_throw()
            {
                // Act
                Action act = () => _sut.Parse(TestValues.InvalidIban);

                // Assert
                IbanFormatException ex = act.Should().Throw<IbanFormatException>("the provided value was invalid").Which;
                ex.Result.Should().BeEquivalentTo(new ValidationResult
                {
                    Error = new IllegalCharactersResult(),
                    AttemptedValue = TestValues.InvalidIban
                });
                ex.InnerException.Should().BeNull();
                ex.Message.Should().Be("The IBAN contains illegal characters.");
            }

            [Fact]
            public void With_valid_value_should_return_iban()
            {
                Iban iban = null;

                // Act
                Action act = () => iban = _sut.Parse(TestValues.ValidIban);

                // Assert
                act.Should().NotThrow<IbanFormatException>();
                iban.Should()
                    .NotBeNull("the value should be parsed")
                    .And.BeOfType<Iban>()
                    .Which.ToString()
                    .Should()
                    .Be(TestValues.ValidIban, "the returned value should match the provided value");
            }

            [Fact]
            public void With_value_that_fails_custom_rule_should_throw()
            {
                // Act
                Action act = () => _sut.Parse(TestValues.IbanForCustomRuleFailure);

                // Assert
                IbanFormatException ex = act.Should().Throw<IbanFormatException>("the provided value was invalid").Which;
                ex.Result.Should().BeEquivalentTo(new ValidationResult
                {
                    Error = new ErrorResult("Custom message"),
                    AttemptedValue = TestValues.IbanForCustomRuleFailure
                });
                ex.InnerException.Should().BeNull();
                ex.Message.Should().Be("Custom message");
            }

            [Fact]
            public void With_value_that_causes_custom_rule_to_throw_should_rethrow()
            {
                // Act
                Action act = () => _sut.Parse(TestValues.IbanForCustomRuleException);

                // Assert
                IbanFormatException ex = act.Should().Throw<IbanFormatException>("the provided value was invalid").Which;
                ex.Result.Should().BeNull();
                ex.InnerException.Should().NotBeNull();
                ex.Message.Should().Contain("is not a valid IBAN.");
                _ibanValidatorStub.Verify(m => m.Validate(TestValues.IbanForCustomRuleException), Times.Once);
            }
        }

        public class When_trying_to_parse_iban : IbanParserTests
        {
            [Fact]
            public void With_null_value_should_return_false()
            {
                // Act
                bool actual = _sut.TryParse(null, out Iban iban);

                // Assert
                actual.Should().BeFalse("the provided value was null which is not valid");
                iban.Should().BeNull("parsing did not succeed");
            }

            [Fact]
            public void With_invalid_value_should_return_false()
            {
                // Act
                bool actual = _sut.TryParse(TestValues.InvalidIban, out Iban iban);

                // Assert
                actual.Should().BeFalse("the provided value was invalid");
                iban.Should().BeNull("parsing did not succeed");

                _ibanValidatorStub.Verify(m => m.Validate(TestValues.InvalidIban), Times.Once);
            }

            [Fact]
            public void With_valid_value_should_pass()
            {
                // Act
                bool actual = _sut.TryParse(TestValues.ValidIban, out Iban iban);

                // Assert
                actual.Should().BeTrue("the provided value was valid");
                iban.Should()
                    .NotBeNull()
                    .And.BeOfType<Iban>()
                    .Which.ToString()
                    .Should()
                    .Be(TestValues.ValidIban);

                _ibanValidatorStub.Verify(m => m.Validate(TestValues.ValidIban), Times.Once);
            }
        }
    }
}

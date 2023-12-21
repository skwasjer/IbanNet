using IbanNet.Registry;
using IbanNet.Validation.Results;
using TestHelpers;

namespace IbanNet;

public class IbanParserTests
{
    private readonly IIbanValidator _ibanValidatorStub;
    private readonly IbanParser _sut;

    protected IbanParserTests()
    {
        _ibanValidatorStub = IbanValidatorStub.Create();
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
        public void Given_invalid_value_when_parsing_it_should_throw(string? attemptedIbanValue, Type expectedExceptionType)
        {
            // Act
            Action act = () => _sut.Parse(attemptedIbanValue!);

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
        public void Given_invalid_value_when_trying_parsing_it_should_not_throw_and_return_false(string? attemptedIbanValue)
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
            IIbanValidator? ibanValidator = null;

            // Act
            Func<IbanParser> parser = () => new IbanParser(ibanValidator!);

            // Assert
            parser.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName(nameof(ibanValidator));
        }

        [Fact]
        public void With_null_registry_it_should_throw()
        {
            IIbanRegistry? registry = null;

            // Act
            Func<IbanParser> parser = () => new IbanParser(registry!);

            // Assert
            parser.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName(nameof(registry));
        }
    }

    public class When_parsing_iban : IbanParserTests
    {
        [Fact]
        public void With_null_value_should_throw()
        {
            string? value = null;

            // Act
            Action act = () => _sut.Parse(value!);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>("the provided value was null")
                .WithParameterName(nameof(value));
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
                Error = new IllegalCharactersResult(0),
                AttemptedValue = TestValues.InvalidIban
            });
            ex.InnerException.Should().BeNull();
            ex.Message.Should().Be("The IBAN contains illegal characters.");
        }

        [Theory]
        [InlineData("NL91 ABNA 0417 1643 00")]
        [InlineData("NL91\tABNA\t0417\t1643\t00")]
        [InlineData(" NL91 ABNA041 716 4300 ")]
        [InlineData("nl91 ABNA041716\t4300")]
        public void Given_that_iban_contains_whitespace_or_lowercase_when_parsing_it_should_succeed(string iban)
        {
            const string expectedNormalizedIban = "NL91ABNA0417164300";
            _ibanValidatorStub
                .Validate(Arg.Any<string>())
                .Returns(new ValidationResult { AttemptedValue = iban, Country = new IbanCountry("NL") });

            // Act
            Iban actual = _sut.Parse(iban);

            // Assert
            actual.ToString().Should().Be(expectedNormalizedIban);
            _ibanValidatorStub.Received(1).Validate(expectedNormalizedIban);
        }

        [Fact]
        public void With_valid_value_should_return_iban()
        {
            Iban? iban = null;

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
            _ibanValidatorStub.Received(1).Validate(TestValues.IbanForCustomRuleException);
        }
    }

    public class When_trying_to_parse_iban : IbanParserTests
    {
        [Fact]
        public void With_null_value_should_return_false()
        {
            // Act
            bool actual = _sut.TryParse(null, out Iban? iban);

            // Assert
            actual.Should().BeFalse("the provided value was null which is not valid");
            iban.Should().BeNull("parsing did not succeed");
        }

        [Fact]
        public void With_invalid_value_should_return_false()
        {
            // Act
            bool actual = _sut.TryParse(TestValues.InvalidIban, out Iban? iban);

            // Assert
            actual.Should().BeFalse("the provided value was invalid");
            iban.Should().BeNull("parsing did not succeed");

            _ibanValidatorStub.Received(1).Validate(TestValues.InvalidIban);
        }

        [Fact]
        public void With_valid_value_should_pass()
        {
            // Act
            bool actual = _sut.TryParse(TestValues.ValidIban, out Iban? iban);

            // Assert
            actual.Should().BeTrue("the provided value was valid");
            iban.Should()
                .NotBeNull()
                .And.BeOfType<Iban>()
                .Which.ToString()
                .Should()
                .Be(TestValues.ValidIban);

            _ibanValidatorStub.Received(1).Validate(TestValues.ValidIban);
        }
    }
}

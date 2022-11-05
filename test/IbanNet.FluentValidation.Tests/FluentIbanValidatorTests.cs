using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using IbanNet.Validation.Results;
using TestHelpers;

namespace IbanNet.FluentValidation;

[Collection(nameof(SetsStaticValidator))]
public class FluentIbanValidatorTests
{
    private readonly IbanValidatorStub _ibanValidatorStub;
    private readonly TestModelValidator _validator;

    protected FluentIbanValidatorTests()
    {
        _ibanValidatorStub = new IbanValidatorStub();
        var sut = new FluentIbanValidator<TestModel>(_ibanValidatorStub);
        _validator = new TestModelValidator(sut);
    }

    public class When_validating_an_invalid_iban : FluentIbanValidatorTests
    {
        private const string AttemptedIbanValue = TestValues.InvalidIban;

        [Fact]
        public void It_should_call_validator()
        {
            var obj = new TestModel { BankAccountNumber = AttemptedIbanValue };

            // Act
            _validator.Validate(obj);

            // Assert
            _ibanValidatorStub.Verify(m => m.Validate(AttemptedIbanValue), Times.Once);
        }

        [Fact]
        public void It_should_fail()
        {
            const string expectedPropertyName = "Bank Account Number";
            string expectedErrorMessage = $"'{expectedPropertyName}' is not a valid IBAN.";
            var obj = new TestModel { BankAccountNumber = AttemptedIbanValue };

            // Act
            TestValidationResult<TestModel> result = _validator.TestValidate(obj);

            // Assert
            ValidationFailure error = result.ShouldHaveValidationErrorFor(x => x.BankAccountNumber)
                .Should()
                .HaveCount(1, "because one validation error should have occurred")
                .And.Subject.First();
            error.FormattedMessagePlaceholderValues.Should()
                .ContainKey("Error")
                .WhoseValue.Should()
                .BeOfType<IllegalCharactersResult>();
            error.ErrorMessage.Should().Be(expectedErrorMessage);
        }
    }

    public class When_validating_a_valid_iban : FluentIbanValidatorTests
    {
        private const string AttemptedIbanValue = TestValues.ValidIban;


        [Fact]
        public void It_should_call_validator()
        {
            var obj = new TestModel { BankAccountNumber = AttemptedIbanValue };

            // Act
            _validator.Validate(obj);

            // Assert
            _ibanValidatorStub.Verify(m => m.Validate(AttemptedIbanValue), Times.Once);
        }

        [Fact]
        public void It_should_succeed()
        {
            var obj = new TestModel { BankAccountNumber = AttemptedIbanValue };

            TestValidationResult<TestModel> result = _validator.TestValidate(obj);

            result.ShouldNotHaveValidationErrorFor(x => x.BankAccountNumber);
        }
    }

    public class When_validating_a_null_value : FluentIbanValidatorTests
    {
        private const string AttemptedIbanValue = null!;


        [Fact]
        public void It_should_not_call_validator()
        {
            var obj = new TestModel { BankAccountNumber = AttemptedIbanValue };

            // Act
            _validator.Validate(obj);

            // Assert
            _ibanValidatorStub.Verify(m => m.Validate(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void It_should_succeed()
        {
            var obj = new TestModel { BankAccountNumber = AttemptedIbanValue };

            TestValidationResult<TestModel> result = _validator.TestValidate(obj);

            result.ShouldNotHaveValidationErrorFor(x => x.BankAccountNumber);
        }
    }

    public class When_validator_is_null
    {
        [Fact]
        public void It_should_throw()
        {
            IIbanValidator? ibanValidator = null;

            // Act
            Func<FluentIbanValidator<TestModel>> act = () => new FluentIbanValidator<TestModel>(ibanValidator!);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(ibanValidator));
        }
    }

    public class When_validator_context_is_null : FluentIbanValidatorTests
    {
        [Fact]
        public void It_should_not_throw_and_validate_successfully()
        {
            var fluentIbanValidator = new FluentIbanValidator<TestModel>(new IbanValidator());
            ValidationContext<TestModel>? context = null;

            // Act
            Func<bool> act = () => fluentIbanValidator.IsValid(context!, string.Empty);

            // Assert
            act.Should().NotThrow();
        }
    }

    private class TestModelValidator : AbstractValidator<TestModel>
    {
        public TestModelValidator(FluentIbanValidator<TestModel> validator)
        {
            RuleFor(x => x.BankAccountNumber).SetValidator(validator);
        }
    }
}

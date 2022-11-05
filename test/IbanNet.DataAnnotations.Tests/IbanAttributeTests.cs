using System.ComponentModel.DataAnnotations;
using IbanNet.Validation.Results;
using TestHelpers;

namespace IbanNet.DataAnnotations;

[Collection(nameof(SetsStaticValidator))]
public class IbanAttributeTests
{
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly IbanAttribute _sut;
    private readonly IbanValidatorStub _ibanValidatorStub;

    private ValidationContext _validationContext;

    protected IbanAttributeTests()
    {
        _ibanValidatorStub = new IbanValidatorStub();
        _serviceProviderMock = new Mock<IServiceProvider>();
        _serviceProviderMock
            .Setup(m => m.GetService(typeof(IIbanValidator)))
            .Returns(_ibanValidatorStub);

        _validationContext = new ValidationContext(new object(), _serviceProviderMock.Object, null);

        _sut = new IbanAttribute();
    }

    public class When_validating_a_null_value : IbanAttributeTests
    {
        [Fact]
        public void It_should_not_call_validator()
        {
            // Act
            _sut.GetValidationResult(null, _validationContext);

            // Assert
            _ibanValidatorStub.VerifyNoOtherCalls();
        }

        [Fact]
        public void It_should_not_resolve_the_validator()
        {
            // Act
            _sut.GetValidationResult(null, _validationContext);

            // Assert
            _serviceProviderMock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Never);
        }

        [Fact]
        public void It_should_succeed()
        {
            // Act
            System.ComponentModel.DataAnnotations.ValidationResult result = _sut.GetValidationResult(null, _validationContext);

            // Assert
            result.Should().Be(System.ComponentModel.DataAnnotations.ValidationResult.Success);
        }

        [Fact]
        public void It_should_not_set_error_item()
        {
            // Act
            _sut.GetValidationResult(null, _validationContext);

            // Assert
            _validationContext.Items.Should().NotContainKey("Error");
        }
    }

    public class When_validating_a_valid_iban : IbanAttributeTests
    {
        [Fact]
        public void It_should_call_validator()
        {
            // Act
            _sut.GetValidationResult(TestValues.ValidIban, _validationContext);

            // Assert
            _ibanValidatorStub.Verify(m => m.Validate(TestValues.ValidIban), Times.Once);
            _ibanValidatorStub.VerifyNoOtherCalls();
        }

        [Fact]
        public void It_should_resolve_the_validator()
        {
            // Act
            _sut.GetValidationResult(TestValues.ValidIban, _validationContext);

            // Assert
            _serviceProviderMock.Verify(m => m.GetService(typeof(IIbanValidator)));
            _serviceProviderMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void It_should_succeed()
        {
            // Act
            System.ComponentModel.DataAnnotations.ValidationResult result = _sut.GetValidationResult(TestValues.ValidIban, _validationContext);

            // Assert
            result.Should().Be(System.ComponentModel.DataAnnotations.ValidationResult.Success);
        }

        [Fact]
        public void It_should_not_set_error_item()
        {
            // Act
            _sut.GetValidationResult(TestValues.ValidIban, _validationContext);

            // Assert
            _validationContext.Items.Should().NotContainKey("Error");
        }
    }

    public class When_validating_an_invalid_iban : IbanAttributeTests
    {
        [Fact]
        public void It_should_call_validator()
        {
            // Act
            _sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

            // Assert
            _ibanValidatorStub.Verify(m => m.Validate(TestValues.InvalidIban), Times.Once);
            _ibanValidatorStub.VerifyNoOtherCalls();
        }

        [Fact]
        public void It_should_fail()
        {
            // Act
            System.ComponentModel.DataAnnotations.ValidationResult result = _sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

            // Assert
            result.Should().NotBe(System.ComponentModel.DataAnnotations.ValidationResult.Success);
        }

        [Fact]
        public void It_should_have_error_message_with_displayName()
        {
            _validationContext.DisplayName = "Property";

            // Act
            System.ComponentModel.DataAnnotations.ValidationResult result = _sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

            // Assert
            result.ErrorMessage.Should().Be(string.Format(Resources.IbanAttribute_Invalid, _validationContext.DisplayName));
        }

        [Fact]
        public void It_should_resolve_the_validator()
        {
            // Act
            _sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

            // Assert
            _serviceProviderMock.Verify(m => m.GetService(typeof(IIbanValidator)));
            _serviceProviderMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void It_should_set_member_name()
        {
            // Act
            System.ComponentModel.DataAnnotations.ValidationResult result = _sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

            // Assert
            if (string.IsNullOrEmpty(_validationContext.MemberName))
            {
                result.MemberNames.Should().BeNullOrEmpty();
            }
            else
            {
                result.MemberNames.Should()
                    .NotBeNull()
                    .And.BeEquivalentTo(_validationContext.MemberName);
            }
        }

        [Fact]
        public void It_should_set_error_item()
        {
            // Act
            _sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

            // Assert
            _validationContext.Items.Should()
                .ContainKey("Error")
                .WhoseValue.Should()
                .BeOfType<IllegalCharactersResult>();
        }
    }

    public class Given_context_with_member_name : When_validating_an_invalid_iban
    {
        public Given_context_with_member_name()
        {
            _validationContext.MemberName = "MyMemberName";
        }
    }

    public class When_validating_an_unsupported_type : IbanAttributeTests
    {
        private static readonly object InvalidTypeValue = new();

        [Fact]
        public void It_should_throw()
        {
            // Act
            Action act = () => _sut.GetValidationResult(InvalidTypeValue, _validationContext);

            // Assert
            act.Should().Throw<NotImplementedException>();

            _serviceProviderMock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Never);
            _ibanValidatorStub.VerifyNoOtherCalls();
        }
    }

    public class When_service_provider_does_not_return_validator : IbanAttributeTests
    {
        [Fact]
        public void It_should_throw()
        {
            // Arrange
            _serviceProviderMock
                .Setup(m => m.GetService(typeof(IIbanValidator)))
                .Returns(null)
                .Verifiable();

            // Act
            Action act = () => _sut.GetValidationResult(TestValues.ValidIban, _validationContext);

            // Assert
            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Failed to get an instance of *");
            _serviceProviderMock.Verify();
            _ibanValidatorStub.VerifyNoOtherCalls();
        }
    }

    public class When_service_provider_is_null : IbanAttributeTests
    {
        [Fact]
        public void It_should_throw()
        {
            // Arrange
            _validationContext = new ValidationContext(new object(), null, null);

            // Act
            Action act = () => _sut.GetValidationResult(TestValues.ValidIban, _validationContext);

            // Assert
            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Failed to get an instance of *");
            _ibanValidatorStub.VerifyNoOtherCalls();
        }
    }

    public class Requires_validation_context : IbanAttributeTests
    {
        [Fact]
        public void It_should_require()
        {
            // We don't support DynamicValidator, as we need context to resolve validator.
            _sut.RequiresValidationContext.Should().BeTrue();
        }
    }
}
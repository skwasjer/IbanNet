using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using IbanNet.Validation.Results;
using Moq;
using NUnit.Framework;

namespace IbanNet.DataAnnotations
{
	[TestFixture]
	internal class IbanAttributeTests : IbanTestFixture
	{
		private Mock<IServiceProvider> _serviceProviderMock;
		private IbanAttribute _sut;

		private ValidationContext _validationContext;

		public override void SetUp()
		{
			base.SetUp();

			_serviceProviderMock = new Mock<IServiceProvider>();
			_serviceProviderMock
				.Setup(m => m.GetService(typeof(IIbanValidator)))
				.Returns(IbanValidatorMock.Object)
				.Verifiable();

			_validationContext = new ValidationContext(new object(), _serviceProviderMock.Object, null);

			_sut = new IbanAttribute();
		}

		public class When_validating_a_null_value : IbanAttributeTests
		{
			[Test]
			public void It_should_not_call_validator()
			{
				// Act
				_sut.GetValidationResult(null, _validationContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(TestValues.ValidIban), Times.Never);
			}

			[Test]
			public void It_should_not_resolve_the_validator()
			{
				// Act
				_sut.GetValidationResult(null, _validationContext);

				// Assert
				_serviceProviderMock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Never);
			}

			[Test]
			public void It_should_succeed()
			{
				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = _sut.GetValidationResult(null, _validationContext);

				// Assert
				result.Should().Be(System.ComponentModel.DataAnnotations.ValidationResult.Success);
			}

			[Test]
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
			[Test]
			public void It_should_call_validator()
			{
				// Act
				_sut.GetValidationResult(TestValues.ValidIban, _validationContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(TestValues.ValidIban), Times.Once);
			}

			[Test]
			public void It_should_resolve_the_validator()
			{
				// Act
				_sut.GetValidationResult(TestValues.ValidIban, _validationContext);

				// Assert
				_serviceProviderMock.Verify();
			}

			[Test]
			public void It_should_succeed()
			{
				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = _sut.GetValidationResult(TestValues.ValidIban, _validationContext);

				// Assert
				result.Should().Be(System.ComponentModel.DataAnnotations.ValidationResult.Success);
			}

			[Test]
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
			[Test]
			public void It_should_call_validator()
			{
				// Act
				_sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(TestValues.InvalidIban), Times.Once);
			}

			[Test]
			public void It_should_fail()
			{
				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = _sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

				// Assert
				result.Should().NotBe(System.ComponentModel.DataAnnotations.ValidationResult.Success);
			}

			[Test]
			public void It_should_have_error_message_with_displayName()
			{
				_validationContext.DisplayName = "Property";

				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = _sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

				// Assert
				result.ErrorMessage.Should().Be(string.Format(Resources.IbanAttribute_Invalid, _validationContext.DisplayName));
			}

			[Test]
			public void It_should_resolve_the_validator()
			{
				// Act
				_sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

				// Assert
				_serviceProviderMock.Verify();
			}

			[Test]
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

			[Test]
			public void It_should_set_error_item()
			{
				// Act
				_sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

				// Assert
				_validationContext.Items.Should()
					.ContainKey("Error")
					.WhichValue.Should()
					.BeOfType<IllegalCharactersResult>();
			}

			public class Given_context_with_member_name : When_validating_an_invalid_iban
			{
				public override void SetUp()
				{
					base.SetUp();

					_validationContext.MemberName = "MyMemberName";
				}
			}
		}

		public class When_validating_an_unsupported_type : IbanAttributeTests
		{
			private static readonly object InvalidTypeValue = new object();

			[Test]
			public void It_should_throw()
			{
				// Act
				Action act = () => _sut.GetValidationResult(InvalidTypeValue, _validationContext);

				// Assert
				act.Should().Throw<NotImplementedException>();

				_serviceProviderMock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Never);
				IbanValidatorMock.Verify(m => m.Validate(TestValues.InvalidIban), Times.Never);
			}
		}

		public class When_no_validator_is_registered : IbanAttributeTests
		{
			[Test]
			public void It_should_throw()
			{
				// Arrange
				_serviceProviderMock.Reset();
				Iban.Validator = null;

				// Act
				Action act = () => _sut.GetValidationResult(TestValues.ValidIban, _validationContext);

				// Assert
				act.Should().Throw<InvalidOperationException>()
					.WithMessage("Failed to get an instance of *");

				_serviceProviderMock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Once);
				IbanValidatorMock.Verify(m => m.Validate(TestValues.InvalidIban), Times.Never);
			}
		}

		public class When_service_provider_does_not_return_validator : IbanAttributeTests
		{
			[Test]
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
				act.Should().Throw<InvalidOperationException>()
					.WithMessage("Failed to get an instance of *");
				_serviceProviderMock.Verify();
				IbanValidatorMock.Verify(m => m.Validate(TestValues.ValidIban), Times.Never);
			}
		}

		public class When_service_provider_is_null : IbanAttributeTests
		{
			[Test]
			public void It_should_throw()
			{
				// Arrange
				_validationContext = new ValidationContext(new object(), null, null);

				// Act
				Action act = () => _sut.GetValidationResult(TestValues.ValidIban, _validationContext);

				// Assert
				act.Should().Throw<InvalidOperationException>()
					.WithMessage("Failed to get an instance of *");
				IbanValidatorMock.Verify(m => m.Validate(TestValues.ValidIban), Times.Never);
			}
		}
	}
}

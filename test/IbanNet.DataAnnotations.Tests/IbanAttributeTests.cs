using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace IbanNet.DataAnnotations
{
	[TestFixture]
	internal class IbanAttributeTests : IbanAttributeTestBase<IbanAttribute>
	{
		protected override IbanAttribute CreateSubject()
		{
			return new IbanAttribute();
		}

		public class When_validating_a_null_value : IbanAttributeTests
		{
			[Test]
			public void It_should_not_call_validator()
			{
				// Act
				Sut.GetValidationResult(null, ValidationContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(TestValues.ValidIban), Times.Never);
			}

			[Test]
			public void It_should_not_resolve_the_validator()
			{
				// Act
				Sut.GetValidationResult(null, ValidationContext);

				// Assert
				ServiceProviderMock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Never);
			}

			[Test]
			public void It_should_succeed()
			{
				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = Sut.GetValidationResult(null, ValidationContext);

				// Assert
				result.Should().Be(System.ComponentModel.DataAnnotations.ValidationResult.Success);
			}
		}

		public class When_validating_a_valid_iban : IbanAttributeTests
		{
			[Test]
			public void It_should_call_validator()
			{
				// Act
				Sut.GetValidationResult(TestValues.ValidIban, ValidationContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(TestValues.ValidIban), Times.Once);
			}

			[Test]
			public void It_should_resolve_the_validator()
			{
				// Act
				Sut.GetValidationResult(TestValues.ValidIban, ValidationContext);

				// Assert
				ServiceProviderMock.Verify();
			}

			[Test]
			public void It_should_succeed()
			{
				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = Sut.GetValidationResult(TestValues.ValidIban, ValidationContext);

				// Assert
				result.Should().Be(System.ComponentModel.DataAnnotations.ValidationResult.Success);
			}
		}

		public class When_validating_an_invalid_iban : IbanAttributeTests
		{
			[Test]
			public void It_should_call_validator()
			{
				// Act
				Sut.GetValidationResult(TestValues.InvalidIban, ValidationContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(TestValues.InvalidIban), Times.Once);
			}

			[Test]
			public void It_should_fail()
			{
				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = Sut.GetValidationResult(TestValues.InvalidIban, ValidationContext);

				// Assert
				result.Should().NotBe(System.ComponentModel.DataAnnotations.ValidationResult.Success);
			}

			[Test]
			public void It_should_have_error_message_with_displayName()
			{
				ValidationContext.DisplayName = "Property";

				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = Sut.GetValidationResult(TestValues.InvalidIban, ValidationContext);

				// Assert
				result.ErrorMessage.Should().Be(string.Format(Resources.IbanAttribute_Invalid, ValidationContext.DisplayName));
			}

			[Test]
			public void It_should_resolve_the_validator()
			{
				// Act
				Sut.GetValidationResult(TestValues.InvalidIban, ValidationContext);

				// Assert
				ServiceProviderMock.Verify();
			}
		}

		public class When_validating_an_unsupported_type : IbanAttributeTests
		{
			private static readonly object InvalidTypeValue = new object();

			[Test]
			public void It_should_throw()
			{
				// Act
				Action act = () => Sut.GetValidationResult(InvalidTypeValue, ValidationContext);

				// Assert
				act.Should().Throw<NotImplementedException>();

				ServiceProviderMock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Never);
				IbanValidatorMock.Verify(m => m.Validate(TestValues.InvalidIban), Times.Never);
			}
		}

		public class When_no_validator_is_registered : IbanAttributeTests
		{
			[Test]
			public void It_should_throw()
			{
				// Arrange
				ServiceProviderMock.Reset();
				Iban.Validator = null;

				// Act
				Action act = () => Sut.GetValidationResult(TestValues.ValidIban, ValidationContext);

				// Assert
				act.Should().Throw<InvalidOperationException>();

				ServiceProviderMock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Once);
				IbanValidatorMock.Verify(m => m.Validate(TestValues.InvalidIban), Times.Never);
			}
		}

		public class When_service_provider_does_not_return_validator : IbanAttributeTests
		{
			[Test]
			public void It_should_use_default_validator()
			{
				// Arrange
				ServiceProviderMock
					.Setup(m => m.GetService(typeof(IIbanValidator)))
					.Returns(null)
					.Verifiable();

				// Act
				Sut.GetValidationResult(TestValues.ValidIban, ValidationContext);

				// Assert
				ServiceProviderMock.Verify();
				IbanValidatorMock.Verify(m => m.Validate(TestValues.ValidIban), Times.Once);
			}
		}
	}
}
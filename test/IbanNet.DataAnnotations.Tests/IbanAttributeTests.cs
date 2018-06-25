using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
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
			public void It_should_resolve_the_validator()
			{
				// Act
				_sut.GetValidationResult(null, _validationContext);

				// Assert
				_serviceProviderMock.Verify();
			}

			[Test]
			public void It_should_succeed()
			{
				// Act
				ValidationResult result = _sut.GetValidationResult(null, _validationContext);

				// Assert
				result.Should().Be(ValidationResult.Success);
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
				ValidationResult result = _sut.GetValidationResult(TestValues.ValidIban, _validationContext);

				// Assert
				result.Should().Be(ValidationResult.Success);
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
			public void It_should_resolve_the_validator()
			{
				// Act
				_sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

				// Assert
				_serviceProviderMock.Verify();
			}

			[Test]
			public void It_should_fail()
			{
				// Act
				ValidationResult result = _sut.GetValidationResult(TestValues.InvalidIban, _validationContext);

				// Assert
				result.Should().NotBe(ValidationResult.Success);
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
				act.Should().Throw<InvalidOperationException>();
			}
		}
	}
}
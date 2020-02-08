using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using IbanNet.Validation.Results;
using Moq;
using TestHelpers;
using Xunit;

namespace IbanNet.FluentValidation
{
	[Collection(nameof(SetsStaticValidator))]
	public class FluentIbanValidatorTests : IbanTestFixture
	{
		private readonly FluentIbanValidator _sut;

		public FluentIbanValidatorTests()
		{
			_sut = new FluentIbanValidator(IbanValidatorMock.Object);
		}

		public class When_validating_an_invalid_iban : FluentIbanValidatorTests
		{
			private const string AttemptedIbanValue = TestValues.InvalidIban;
			private readonly PropertyValidatorContext _propertyValidatorContext;

			public When_validating_an_invalid_iban()
			{
				var rule = PropertyRule.Create<TestModel, string>(x => x.BankAccountNumber);

				_propertyValidatorContext = new PropertyValidatorContext(null, rule, null, AttemptedIbanValue);
			}

			[Fact]
			public void It_should_call_validator()
			{
				// Act
				_sut.Validate(_propertyValidatorContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(AttemptedIbanValue), Times.Once);
			}

			[Fact]
			public void It_should_fail()
			{
				const string expectedPropertyName = "Bank Account Number";
				string expectedErrorMessage = $"'{expectedPropertyName}' is not a valid IBAN.";

				// Act
				IEnumerable<ValidationFailure> actual = _sut.Validate(_propertyValidatorContext);

				// Assert
				ValidationFailure error = actual.Should()
					.HaveCount(1, "because one validation error should have occurred")
					.And.Subject.First();
				error.FormattedMessagePlaceholderValues.Should()
					.ContainKey("Error")
					.WhichValue.Should()
					.BeOfType<IllegalCharactersResult>();
				error.ErrorMessage.Should().Be(expectedErrorMessage);
			}
		}

		public class When_validating_a_valid_iban : FluentIbanValidatorTests
		{
			private const string AttemptedIbanValue = TestValues.ValidIban;
			private readonly PropertyValidatorContext _propertyValidatorContext;

			public When_validating_a_valid_iban()
			{
				_propertyValidatorContext = new PropertyValidatorContext(null, PropertyRule.Create<string, object>(_ => null), null, AttemptedIbanValue);
			}

			[Fact]
			public void It_should_call_validator()
			{
				// Act
				_sut.Validate(_propertyValidatorContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(AttemptedIbanValue), Times.Once);
			}

			[Fact]
			public void It_should_succeed()
			{
				// Act
				IEnumerable<ValidationFailure> actual = _sut.Validate(_propertyValidatorContext);

				// Assert
				actual.Should().BeEmpty($"because no validation errors should have occurred");
			}
		}

		public class When_validating_a_null_value : FluentIbanValidatorTests
		{
			private const string AttemptedIbanValue = null;
			private readonly PropertyValidatorContext _propertyValidatorContext;

			public When_validating_a_null_value()
			{
				_propertyValidatorContext = new PropertyValidatorContext(null, PropertyRule.Create<string, object>(_ => null), null, AttemptedIbanValue);
			}

			[Fact]
			public void It_should_not_call_validator()
			{
				// Act
				_sut.Validate(_propertyValidatorContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(It.IsAny<string>()), Times.Never);
			}

			[Fact]
			public void It_should_succeed()
			{
				// Act
				IEnumerable<ValidationFailure> actual = _sut.Validate(_propertyValidatorContext);

				// Assert
				actual.Should().BeEmpty("because a null iban is valid");
			}
		}

		public class When_validating_an_unsupported_type : FluentIbanValidatorTests
		{
			private static readonly object InvalidTypeValue = new object();
			private readonly PropertyValidatorContext _propertyValidatorContext;

			public When_validating_an_unsupported_type()
			{
				_propertyValidatorContext = new PropertyValidatorContext(null, PropertyRule.Create<string, object>(_ => null), null, InvalidTypeValue);
			}

			[Fact]
			public void It_should_throw()
			{
				// Act
				Action act = () => _sut.Validate(_propertyValidatorContext);

				// Assert
				act.Should().Throw<InvalidCastException>();
				IbanValidatorMock.Verify(m => m.Validate(It.IsAny<string>()), Times.Never);
			}
		}

		public class When_validator_is_null
		{
			[Fact]
			public void It_should_throw()
			{
				IIbanValidator ibanValidator = null;

				// Act
				// ReSharper disable once ExpressionIsAlwaysNull
				Action act = () => new FluentIbanValidator(ibanValidator);

				// Assert
				act.Should()
					.Throw<ArgumentNullException>()
					.Which.ParamName.Should()
					.Be(nameof(ibanValidator));
			}
		}
	}
}

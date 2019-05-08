using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using Moq;
using NUnit.Framework;

namespace IbanNet.FluentValidation
{
	[TestFixture]
	internal class FluentIbanValidatorTests : IbanTestFixture
	{
		private FluentIbanValidator _sut;

		public override void SetUp()
		{
			base.SetUp();

			_sut = new FluentIbanValidator(IbanValidatorMock.Object);
		}

		public class When_validating_an_invalid_iban : FluentIbanValidatorTests
		{
			private const string AttemptedIbanValue = TestValues.InvalidIban;
			private PropertyValidatorContext _propertyValidatorContext;

			public override void SetUp()
			{
				base.SetUp();

				PropertyRule rule = PropertyRule.Create<TestModel, string>(x => x.BankAccountNumber);

				_propertyValidatorContext = new PropertyValidatorContext(null, rule, null, AttemptedIbanValue);
			}

			[Test]
			public void It_should_call_validator()
			{
				// Act
				_sut.Validate(_propertyValidatorContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(AttemptedIbanValue), Times.Once);
			}

			[Test]
			public void It_should_fail()
			{
				const string expectedPropertyName = "Bank Account Number";
				string expectedErrorMessage = $"'{expectedPropertyName}' is not a valid IBAN.";

				// Act
				IEnumerable<ValidationFailure> actual = _sut.Validate(_propertyValidatorContext);

				// Assert
				actual.Should()
					.HaveCount(1, "because one validation error should have occurred")
					.And.Subject.First()
					.ErrorMessage.Should()
					.Be(expectedErrorMessage);
			}
		}

		public class When_validating_a_valid_iban : FluentIbanValidatorTests
		{
			private const string AttemptedIbanValue = TestValues.ValidIban;
			private PropertyValidatorContext _propertyValidatorContext;

			public override void SetUp()
			{
				base.SetUp();

				_propertyValidatorContext = new PropertyValidatorContext(null, PropertyRule.Create<string, object>(_ => null), null, AttemptedIbanValue);
			}

			[Test]
			public void It_should_call_validator()
			{
				// Act
				_sut.Validate(_propertyValidatorContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(AttemptedIbanValue), Times.Once);
			}

			[Test]
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
			private PropertyValidatorContext _propertyValidatorContext;

			public override void SetUp()
			{
				base.SetUp();

				_propertyValidatorContext = new PropertyValidatorContext(null, PropertyRule.Create<string, object>(_ => null), null, AttemptedIbanValue);
			}

			[Test]
			public void It_should_not_call_validator()
			{
				// Act
				_sut.Validate(_propertyValidatorContext);

				// Assert
				IbanValidatorMock.Verify(m => m.Validate(It.IsAny<string>()), Times.Never);
			}

			[Test]
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
			private PropertyValidatorContext _propertyValidatorContext;

			public override void SetUp()
			{
				base.SetUp();

				_propertyValidatorContext = new PropertyValidatorContext(null, PropertyRule.Create<string, object>(_ => null), null, InvalidTypeValue);
			}

			[Test]
			public void It_should_throw()
			{
				// Act
				Action act = () => _sut.Validate(_propertyValidatorContext);

				// Assert
				act.Should().Throw<InvalidCastException>();
				IbanValidatorMock.Verify(m => m.Validate(It.IsAny<string>()), Times.Never);
			}
		}
	}
}
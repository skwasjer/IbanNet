using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;

namespace IbanNet.FluentValidation
{
	[TestFixture]
	internal class IntegrationTests : IbanTestFixture
	{
		private TestModelValidator _sut;
		private TestModel _testModel;

		public override void SetUp()
		{
			base.SetUp();

			_sut = new TestModelValidator(new IbanValidator());

			_testModel = new TestModel();
		}

		[TestCase("NL91 ABNA 0417")]
		[TestCase("GB29 NWBK 6016")]
		public void Given_a_model_with_invalid_iban_when_validating_should_contain_validation_errors(string attemptedIbanValue)
		{
			_testModel.BankAccountNumber = attemptedIbanValue;

			const string expectedFormattedPropertyName = "Bank Account Number";
			var expectedValidationFailure = new ValidationFailure(nameof(_testModel.BankAccountNumber), $"'{expectedFormattedPropertyName}' is not a valid IBAN.")
			{
				AttemptedValue = attemptedIbanValue,
				ErrorCode = nameof(FluentIbanValidator),
				FormattedMessageArguments = new object[0],
				FormattedMessagePlaceholderValues = new Dictionary<string, object>
				{
					{ "PropertyName", expectedFormattedPropertyName },
					{ "PropertyValue", attemptedIbanValue }
				}
			};

			// Act
			var actual = _sut.Validate(_testModel);

			// Assert
			actual.IsValid.Should().BeFalse("because one validation error should have occurred");
			actual.Errors.Should()
				.HaveCount(1, "because one validation error should have occurred")
				.And.Subject.Single()
				.Should().BeEquivalentTo(expectedValidationFailure);
		}

		[TestCase("NL91 ABNA 0417 1643 00")]
		[TestCase("GB29 NWBK 6016 1331 9268 19")]
		public void Given_a_model_with_iban_when_validating_should_not_contain_validation_errors(string attemptedIbanValue)
		{
			_testModel.BankAccountNumber = attemptedIbanValue;

			// Act
			var actual = _sut.Validate(_testModel);

			// Assert
			actual.IsValid.Should().BeTrue("because no validation errors should have occurred");
		}

		[TestCase("DE89 3704 0044 0532 0130 00")]
		[TestCase("GE29 NB00 0000 0101 9049 17")]
		public void Given_a_model_with_disallowed_iban_when_validating_should_contain_validation_errors(string attemptedIbanValue)
		{
			_testModel.BankAccountNumber = attemptedIbanValue;

			const string expectedFormattedPropertyName = "Bank Account Number";
			var expectedValidationFailure = new ValidationFailure(nameof(_testModel.BankAccountNumber), $"'{expectedFormattedPropertyName}' is not valid because it is not an IBAN from a list of accepted countries.")
			{
				AttemptedValue = attemptedIbanValue,
				ErrorCode = nameof(FluentIbanCountryValidator),
				FormattedMessageArguments = new object[0],
				FormattedMessagePlaceholderValues = new Dictionary<string, object>
				{
					{ "PropertyName", expectedFormattedPropertyName },
					{ "PropertyValue", attemptedIbanValue }
				}
			};

			// Act
			var actual = _sut.Validate(_testModel);

			// Assert
			actual.IsValid.Should().BeFalse("because one validation error should have occurred");
			actual.Errors.Should()
				.HaveCount(1, "because one validation error should have occurred")
				.And.Subject.Single()
				.Should().BeEquivalentTo(expectedValidationFailure);
		}

		[TestCase("DE89 3704 0044")]
		[TestCase("GE29 NB00 0000")]
		public void Given_a_model_with_disallowed_country_and_invalid_iban_when_validating_should_contain_only_allowed_country_validation_errors(string attemptedIbanValue)
		{
			_testModel.BankAccountNumber = attemptedIbanValue;

			const string expectedFormattedPropertyName = "Bank Account Number";
			var expectedValidationFailure = new ValidationFailure(nameof(_testModel.BankAccountNumber), $"'{expectedFormattedPropertyName}' is not valid because it is not an IBAN from a list of accepted countries.")
			{
				AttemptedValue = attemptedIbanValue,
				ErrorCode = nameof(FluentIbanCountryValidator),
				FormattedMessageArguments = new object[0],
				FormattedMessagePlaceholderValues = new Dictionary<string, object>
				{
					{ "PropertyName", expectedFormattedPropertyName },
					{ "PropertyValue", attemptedIbanValue }
				}
			};

			// Act
			var actual = _sut.Validate(_testModel);

			// Assert
			actual.IsValid.Should().BeFalse("because one validation error should have occurred");
			actual.Errors.Should()
				.HaveCount(1, "because one validation error should have occurred")
				.And.Subject.Single()
				.Should().BeEquivalentTo(expectedValidationFailure);
		}

		private class TestModelValidator : AbstractValidator<TestModel>
		{
			public TestModelValidator(IIbanValidator ibanValidator)
			{
				RuleFor(x => x.BankAccountNumber)
					.Iban(new IbanValidationOptions
						{
							Validator = ibanValidator,
							CountryCodes =
							{
								"NL", "GB"
							}
						}
					);
			}
		}
	}
}
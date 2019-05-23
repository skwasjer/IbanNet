using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IbanNet.DataAnnotations
{
	[TestFixture]
	internal class AttributeIntegrationTests
	{
		private IServiceProvider _serviceProvider;

		public class Model
		{
			[Iban]
			[IbanCountry("NL", "GB")]
			public string BankAccount { get; set; }
		}

		[SetUp]
		public virtual void SetUp()
		{
			_serviceProvider = new ServiceCollection()
				.AddSingleton(Iban.Validator)
				.BuildServiceProvider();
		}

		[TestCase("NL91 ABNA 0417 1643 00")]
		[TestCase("GB29 NWBK 6016 1331 9268 19")]
		public void Given_allowed_iban_when_validating_should_have_no_errors(string iban)
		{
			var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
			var sut = new Model { BankAccount = iban };

			var validationContext = new ValidationContext(sut, _serviceProvider, null);

			// Act
			Validator.TryValidateObject(sut, validationContext, validationResults, true);

			// Assert
			validationResults.Should().BeEmpty();
		}

		[TestCase("DE89 3704 0044 0532 0130 00")]
		[TestCase("GE29 NB00 0000 0101 9049 17")]
		public void Given_disallowed_iban_when_validating_should_have_one_error(string iban)
		{
			string expectedError = Resources.IbanCountryAttribute_NotAccepted;
			var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
			var sut = new Model { BankAccount = iban };
			var expectedValidationResult = new System.ComponentModel.DataAnnotations.ValidationResult(
				string.Format(expectedError, nameof(Model.BankAccount)),
				new[] { nameof(Model.BankAccount) }
			);

			var validationContext = new ValidationContext(sut, _serviceProvider, null);

			// Act
			Validator.TryValidateObject(sut, validationContext, validationResults, true);

			// Assert
			validationResults.Should().BeEquivalentTo(expectedValidationResult);
		}

		[TestCase("NL91 ABNA 0417")]
		[TestCase("GB29 NWBK 6016")]
		public void Given_invalid_iban_but_allowed_country_when_validating_should_have_one_error(string iban)
		{
			string expectedError = Resources.IbanAttribute_Invalid;
			var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
			var sut = new Model { BankAccount = iban };
			var expectedValidationResult = new System.ComponentModel.DataAnnotations.ValidationResult(
				string.Format(expectedError, nameof(Model.BankAccount)),
				new[] { nameof(Model.BankAccount) }
			);

			var validationContext = new ValidationContext(sut, _serviceProvider, null);

			// Act
			Validator.TryValidateObject(sut, validationContext, validationResults, true);

			// Assert
			validationResults.Should().BeEquivalentTo(expectedValidationResult);
		}

		[TestCase("DE89 3704 0044")]
		[TestCase("GE29 NB00 0000")]
		public void Given_invalid_iban_and_disallowed_country_when_validating_should_have_two_errors(string iban)
		{
			var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
			var sut = new Model { BankAccount = iban };
			var invalidIbanResult = new System.ComponentModel.DataAnnotations.ValidationResult(
				string.Format(Resources.IbanAttribute_Invalid, nameof(Model.BankAccount)),
				new[] { nameof(Model.BankAccount) }
			);
			var notAcceptedCountryResult = new System.ComponentModel.DataAnnotations.ValidationResult(
				string.Format(Resources.IbanCountryAttribute_NotAccepted, nameof(Model.BankAccount)),
				new[] { nameof(Model.BankAccount) }
			);

			var validationContext = new ValidationContext(sut, _serviceProvider, null);

			// Act
			Validator.TryValidateObject(sut, validationContext, validationResults, true);

			// Assert
			validationResults.Should().BeEquivalentTo(invalidIbanResult, notAcceptedCountryResult);
		}
	}
}
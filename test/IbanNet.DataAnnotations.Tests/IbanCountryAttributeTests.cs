using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace IbanNet.DataAnnotations
{
	[TestFixture]
	internal class IbanCountryAttributeTests : IbanAttributeTestBase<IbanCountryAttribute>
	{
		protected override IbanCountryAttribute CreateSubject()
		{
			return new IbanCountryAttribute("NL", "GB", TestValues.ValidIban.Substring(0, 2));
		}

		public class When_validating_a_null_value : IbanCountryAttributeTests
		{
			[Test]
			public void It_should_succeed()
			{
				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = Sut.GetValidationResult(null, ValidationContext);

				// Assert
				result.Should().Be(System.ComponentModel.DataAnnotations.ValidationResult.Success);
			}
		}

		public class When_validating_an_iban : IbanCountryAttributeTests
		{
			[Test]
			public void It_should_succeed()
			{
				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = Sut.GetValidationResult(TestValues.ValidIban, ValidationContext);

				// Assert
				result.Should().Be(System.ComponentModel.DataAnnotations.ValidationResult.Success);
			}
		}

		public class Given_country_code_is_not_accepted_when_validating_an_iban : IbanCountryAttributeTests
		{
			protected override IbanCountryAttribute CreateSubject()
			{
				return new IbanCountryAttribute("XX");
			}

			[Test]
			public void It_should_fail()
			{
				// Act
				System.ComponentModel.DataAnnotations.ValidationResult result = Sut.GetValidationResult(TestValues.ValidIban, ValidationContext);

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
				result.ErrorMessage.Should().Be(string.Format(Resources.IbanCountryAttribute_NotAccepted, ValidationContext.DisplayName));
			}
		}

		public class When_validating_an_unsupported_type : IbanCountryAttributeTests
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
	}
}
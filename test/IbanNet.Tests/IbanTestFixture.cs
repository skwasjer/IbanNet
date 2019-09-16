using System;
using Moq;
using NUnit.Framework;

namespace IbanNet
{
	internal class IbanTestFixture
	{
		protected Mock<IIbanValidator> IbanValidatorMock;

		[SetUp]
		public virtual void SetUp()
		{
			IbanValidatorMock = new Mock<IIbanValidator>();
			IbanValidatorMock
				.Setup(m => m.Validate(It.IsAny<string>()))
				.Returns<string>(iban => new ValidationResult
				{
					Value = iban,
					Result = IbanValidationResult.Valid
				});
			IbanValidatorMock
				.Setup(m => m.Validate(TestValues.InvalidIban))
				.Returns<string>(iban => new ValidationResult
				{
					Value = iban,
					Result = IbanValidationResult.IllegalCharacters
				});

			IbanValidatorMock
				.Setup(m => m.Validate(TestValues.IbanForCustomRuleFailure))
				.Returns<string>(iban => new ValidationResult
				{
					Value = iban,
					Result = IbanValidationResult.Custom,
					ErrorMessage = "Custom message",
					Exception = new Exception("Some exception")
				});

			Iban.Validator = IbanValidatorMock.Object;
		}
	}
}
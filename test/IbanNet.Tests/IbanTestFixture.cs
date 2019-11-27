using System;
using IbanNet.Validation.Results;
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
					Result = ValidationRuleResult.Success
				});

			IbanValidatorMock
				.Setup(m => m.Validate(null))
				.Returns<string>(iban => new ValidationResult
				{
					Value = null,
					Result = new InvalidLengthResult()
				});

			IbanValidatorMock
				.Setup(m => m.Validate(TestValues.InvalidIban))
				.Returns<string>(iban => new ValidationResult
				{
					Value = iban,
					Result = new IllegalCharactersResult()
				});

			IbanValidatorMock
				.Setup(m => m.Validate(TestValues.IbanForCustomRuleFailure))
				.Returns<string>(iban => new ValidationResult
				{
					Value = iban,
					Result = new ErrorResult("Custom message")
				});

			IbanValidatorMock
				.Setup(m => m.Validate(TestValues.IbanForCustomRuleException))
				.Throws(new InvalidOperationException("Custom message"));

			Iban.Validator = IbanValidatorMock.Object;
		}
	}
}

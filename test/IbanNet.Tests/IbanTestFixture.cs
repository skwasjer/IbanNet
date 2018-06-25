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
				.Setup(m => m.Validate(TestValues.ValidIban))
				.Returns(IbanValidationResult.Valid);
			IbanValidatorMock
				.Setup(m => m.Validate(TestValues.InvalidIban))
				.Returns(IbanValidationResult.IllegalCharacters);

			Iban.Validator = IbanValidatorMock.Object;
		}
	}
}
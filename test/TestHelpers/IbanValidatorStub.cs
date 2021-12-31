using IbanNet;
using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace TestHelpers
{
    public class IbanValidatorStub : Mock<IIbanValidator>, IIbanValidator
    {
        public IbanValidatorStub()
        {
            Setup(m => m.Validate(It.IsAny<string>()))
                .Returns<string>(iban => new ValidationResult
                {
                    AttemptedValue = iban,
                    Country = IbanRegistry.Default[iban.Substring(0, 2)]
                });

            Setup(m => m.Validate(null))
                .Returns(new ValidationResult { AttemptedValue = null, Error = new InvalidLengthResult() });

            Setup(m => m.Validate(TestValues.InvalidIban))
                .Returns<string>(iban => new ValidationResult { AttemptedValue = iban, Error = new IllegalCharactersResult() });

            Setup(m => m.Validate(TestValues.IbanForCustomRuleFailure))
                .Returns<string>(iban => new ValidationResult { AttemptedValue = iban, Error = new ErrorResult("Custom message") });

            Setup(m => m.Validate(TestValues.IbanForCustomRuleException))
                .Throws(new InvalidOperationException("Custom message"));
        }

        public ValidationResult Validate(string iban)
        {
            return Object.Validate(iban);
        }
    }
}

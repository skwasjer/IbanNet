using IbanNet;
using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace TestHelpers;

public static class IbanValidatorStub
{
    public static IIbanValidator Create()
    {
        IIbanValidator mock = Substitute.For<IIbanValidator>();
        mock.Validate(Arg.Any<string>())
            .Returns(info =>
            {
                string? iban = info.Arg<string>();
                return iban switch
                {
                    null => new ValidationResult { AttemptedValue = null, Error = new InvalidLengthResult() },
                    TestValues.InvalidIban => new ValidationResult { AttemptedValue = info.Arg<string>(), Error = new IllegalCharactersResult(0) },
                    TestValues.IbanForCustomRuleFailure => new ValidationResult { AttemptedValue = info.Arg<string>(), Error = new ErrorResult("Custom message") },
                    TestValues.IbanForCustomRuleException => throw new InvalidOperationException("Custom message"),
                    _ => new ValidationResult { AttemptedValue = iban, Country = IbanRegistry.Default[iban.Substring(0, 2)] }
                };
            });

        return mock;
    }
}

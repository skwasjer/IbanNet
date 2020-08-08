namespace IbanNet
{
    public class LooseIbanValidatorTests : IbanValidatorIntegrationTests
    {
        public LooseIbanValidatorTests()
            : base(new IbanValidator(new IbanValidatorOptions { Method = ValidationMethod.Loose }))
        {
        }
    }
}

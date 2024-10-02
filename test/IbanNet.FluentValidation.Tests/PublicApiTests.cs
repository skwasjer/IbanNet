using TestHelpers.Specs;

namespace IbanNet.FluentValidation;

public sealed class PublicApiTests : PublicApiSpec
{
    public PublicApiTests()
        : base(typeof(FluentIbanValidator<>))
    {
    }
}

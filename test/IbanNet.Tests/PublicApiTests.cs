using TestHelpers.Specs;

namespace IbanNet;

public sealed class PublicApiTests : PublicApiSpec
{
    public PublicApiTests()
        : base(typeof(Iban))
    {
    }
}

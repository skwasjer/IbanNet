using TestHelpers.Specs;

namespace IbanNet.DataAnnotations;

public sealed class PublicApiTests : PublicApiSpec
{
    public PublicApiTests()
        : base(typeof(IbanAttribute))
    {
    }
}

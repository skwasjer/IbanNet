using TestHelpers.Specs;

namespace IbanNet.DependencyInjection.ServiceProvider;

public sealed class PublicApiTests : PublicApiSpec
{
    public PublicApiTests()
        : base(typeof(ServiceCollectionExtensions))
    {
    }
}

using TestHelpers.Specs;

namespace IbanNet.DependencyInjection.Autofac;

public sealed class PublicApiTests : PublicApiSpec
{
    public PublicApiTests()
        : base(typeof(AutofacRegistrationExtensions))
    {
    }
}

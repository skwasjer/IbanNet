using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs;

public class ShouldUseDefaultRegistrySpec : TestHelpers.Specs.ShouldUseDefaultRegistrySpec
{
    public ShouldUseDefaultRegistrySpec() : base(new ServiceProviderDependencyInjectionFixture(true))
    {
    }
}
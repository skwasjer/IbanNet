using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs;

public class ShouldResolveRegistry : TestHelpers.Specs.ShouldResolveRegistry
{
    public ShouldResolveRegistry() : base(new ServiceProviderDependencyInjectionFixture(true))
    {
    }
}
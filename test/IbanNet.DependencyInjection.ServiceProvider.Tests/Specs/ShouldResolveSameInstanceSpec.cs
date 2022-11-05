using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs;

public class ShouldResolveSameInstanceSpec : TestHelpers.Specs.ShouldResolveSameInstanceSpec
{
    public ShouldResolveSameInstanceSpec() : base(new ServiceProviderDependencyInjectionFixture(true))
    {
    }
}
using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs;

public class ShouldPreserveStaticValidatorSpec : TestHelpers.Specs.ShouldPreserveStaticValidatorSpec
{
    public ShouldPreserveStaticValidatorSpec() : base(new ServiceProviderDependencyInjectionFixture(true))
    {
    }
}
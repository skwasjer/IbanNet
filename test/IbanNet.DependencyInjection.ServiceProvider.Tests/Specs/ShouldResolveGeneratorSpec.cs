using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs
{
    public class ShouldResolveGeneratorSpec : TestHelpers.Specs.ShouldResolveGeneratorSpec
    {
        public ShouldResolveGeneratorSpec() : base(new ServiceProviderDependencyInjectionFixture(true))
        {
        }
    }
}

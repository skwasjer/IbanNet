using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs
{
    public class ConfiguredValidationMethodSpec : TestHelpers.Specs.ConfiguredValidationMethodSpec
    {
        public ConfiguredValidationMethodSpec() : base(new ServiceProviderDependencyInjectionFixture(true))
        {
        }
    }
}

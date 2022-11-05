using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs;

public class ConfiguredMultipleRulesSpec : TestHelpers.Specs.ConfiguredMultipleRulesSpec
{
    public ConfiguredMultipleRulesSpec() : base(new ServiceProviderDependencyInjectionFixture(true))
    {
    }
}
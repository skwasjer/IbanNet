using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs;

public class ConfiguredRuleSpec : TestHelpers.Specs.ConfiguredRuleSpec
{
    public ConfiguredRuleSpec() : base(new ServiceProviderDependencyInjectionFixture(true))
    {
    }
}
using IbanNet.DependencyInjection.Autofac.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Specs;

public class ConfiguredMultipleRulesSpec : TestHelpers.Specs.ConfiguredMultipleRulesSpec
{
    public ConfiguredMultipleRulesSpec() : base(new AutofacDependencyInjectionFixture(true))
    {
    }
}
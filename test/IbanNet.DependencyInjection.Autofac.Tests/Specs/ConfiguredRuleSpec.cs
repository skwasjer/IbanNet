using IbanNet.DependencyInjection.Autofac.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Specs
{
    public class ConfiguredRuleSpec : TestHelpers.Specs.ConfiguredRuleSpec
    {
        public ConfiguredRuleSpec() : base(new AutofacDependencyInjectionFixture(true))
        {
        }
    }
}

using IbanNet.DependencyInjection.Autofac.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Specs
{
    public class ConfiguredRegistrySpec : TestHelpers.Specs.ConfiguredRegistrySpec
    {
        public ConfiguredRegistrySpec() : base(new AutofacDependencyInjectionFixture(true))
        {
        }
    }
}

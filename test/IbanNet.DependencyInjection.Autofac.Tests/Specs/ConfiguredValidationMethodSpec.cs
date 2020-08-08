using IbanNet.DependencyInjection.Autofac.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Specs
{
    public class ConfiguredValidationMethodSpec : TestHelpers.Specs.ConfiguredValidationMethodSpec
    {
        public ConfiguredValidationMethodSpec() : base(new AutofacDependencyInjectionFixture(true))
        {
        }
    }
}

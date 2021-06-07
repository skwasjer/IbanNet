using IbanNet.DependencyInjection.Autofac.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Specs
{
    public class ShouldResolveGeneratorSpec : TestHelpers.Specs.ShouldResolveGeneratorSpec
    {
        public ShouldResolveGeneratorSpec() : base(new AutofacDependencyInjectionFixture(true))
        {
        }
    }
}

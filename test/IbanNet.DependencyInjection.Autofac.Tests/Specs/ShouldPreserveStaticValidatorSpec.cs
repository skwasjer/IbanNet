using IbanNet.DependencyInjection.Autofac.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Specs
{
    public class ShouldPreserveStaticValidatorSpec : TestHelpers.Specs.ShouldPreserveStaticValidatorSpec
    {
        public ShouldPreserveStaticValidatorSpec() : base(new AutofacDependencyInjectionFixture(true))
        {
        }
    }
}

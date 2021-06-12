using IbanNet.DependencyInjection.Autofac.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Specs
{
    public class ShouldSetStaticValidatorSpec : TestHelpers.Specs.ShouldSetStaticValidatorSpec
    {
        public ShouldSetStaticValidatorSpec() : base(new AutofacDependencyInjectionFixture(false))
        {
        }
    }
}

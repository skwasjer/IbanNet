using IbanNet.DependencyInjection.Autofac.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Specs
{
    public class ShouldResolveSameInstanceSpec : TestHelpers.Specs.ShouldResolveSameInstanceSpec
    {
        public ShouldResolveSameInstanceSpec() : base(new AutofacDependencyInjectionFixture(true))
        {
        }
    }
}

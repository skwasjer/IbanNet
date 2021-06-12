using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs
{
    public class ShouldSetStaticValidatorSpec : TestHelpers.Specs.ShouldSetStaticValidatorSpec
    {
        public ShouldSetStaticValidatorSpec() : base(new ServiceProviderDependencyInjectionFixture(false))
        {
        }
    }
}

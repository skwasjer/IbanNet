using IbanNet.DependencyInjection.ServiceProvider.Fixtures;
using TestHelpers;
using Xunit;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs
{
    [Collection(nameof(SetsStaticValidator))]
    public class ShouldPreserveStaticValidatorSpec : TestHelpers.Specs.ShouldPreserveStaticValidatorSpec
    {
        public ShouldPreserveStaticValidatorSpec() : base(new ServiceProviderDependencyInjectionFixture(true))
        {
        }
    }
}

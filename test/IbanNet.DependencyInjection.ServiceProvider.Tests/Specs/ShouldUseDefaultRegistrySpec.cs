using IbanNet.DependencyInjection.ServiceProvider.Fixtures;
using TestHelpers;
using Xunit;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs
{
	public class ShouldUseDefaultRegistrySpec : TestHelpers.Specs.ShouldUseDefaultRegistrySpec
	{
		public ShouldUseDefaultRegistrySpec() : base(new ServiceProviderDependencyInjectionFixture(true))
		{
		}
	}
}

using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs
{
	public class ConfiguredRegistrySpec : TestHelpers.Specs.ConfiguredRegistrySpec
	{
		public ConfiguredRegistrySpec() : base(new ServiceProviderDependencyInjectionFixture(true))
		{
		}
	}
}

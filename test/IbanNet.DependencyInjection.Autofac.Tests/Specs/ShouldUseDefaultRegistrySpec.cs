using IbanNet.DependencyInjection.Autofac.Fixtures;
using TestHelpers;
using Xunit;

namespace IbanNet.DependencyInjection.Autofac.Specs
{
	public class ShouldUseDefaultRegistrySpec : TestHelpers.Specs.ShouldUseDefaultRegistrySpec
	{
		public ShouldUseDefaultRegistrySpec() : base(new AutofacDependencyInjectionFixture(true))
		{
		}
	}
}

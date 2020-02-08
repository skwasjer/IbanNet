using IbanNet.DependencyInjection.ServiceProvider.Fixtures;
using TestHelpers;
using Xunit;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs
{
	[Collection(nameof(SetsStaticValidator))]
	public class ShouldSetStaticValidatorSpec : TestHelpers.Specs.ShouldSetStaticValidatorSpec
	{
		public ShouldSetStaticValidatorSpec() : base(new ServiceProviderDependencyInjectionFixture(false))
		{
		}
	}
}

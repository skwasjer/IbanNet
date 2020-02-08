using IbanNet.DependencyInjection.Autofac.Fixtures;
using TestHelpers;
using Xunit;

namespace IbanNet.DependencyInjection.Autofac.Specs
{
	[Collection(nameof(SetsStaticValidator))]
	public class ShouldSetStaticValidatorSpec : TestHelpers.Specs.ShouldSetStaticValidatorSpec
	{
		public ShouldSetStaticValidatorSpec() : base(new AutofacDependencyInjectionFixture(false))
		{
		}
	}
}

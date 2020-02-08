using IbanNet.DependencyInjection.Autofac.Fixtures;
using TestHelpers;
using Xunit;

namespace IbanNet.DependencyInjection.Autofac.Specs
{
	[Collection(nameof(SetsStaticValidator))]
	public class ShouldPreserveStaticValidatorSpec : TestHelpers.Specs.ShouldPreserveStaticValidatorSpec
	{
		public ShouldPreserveStaticValidatorSpec() : base(new AutofacDependencyInjectionFixture(true))
		{
		}
	}
}

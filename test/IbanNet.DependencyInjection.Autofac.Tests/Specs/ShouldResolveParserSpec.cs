using IbanNet.DependencyInjection.Autofac.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Specs;

public class ShouldResolveParserSpec : TestHelpers.Specs.ShouldResolveParserSpec
{
    public ShouldResolveParserSpec() : base(new AutofacDependencyInjectionFixture(true))
    {
    }
}
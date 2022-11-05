using IbanNet.DependencyInjection.ServiceProvider.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs;

public class ShouldResolveParserSpec : TestHelpers.Specs.ShouldResolveParserSpec
{
    public ShouldResolveParserSpec() : base(new ServiceProviderDependencyInjectionFixture(true))
    {
    }
}
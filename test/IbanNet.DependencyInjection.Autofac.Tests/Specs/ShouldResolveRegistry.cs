using IbanNet.DependencyInjection.Autofac.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Specs;

public class ShouldResolveRegistry : TestHelpers.Specs.ShouldResolveRegistry
{
    public ShouldResolveRegistry() : base(new AutofacDependencyInjectionFixture(true))
    {
    }
}
using Autofac;
using IbanNet.DependencyInjection.Autofac.Fixtures;
using IbanNet.Registry;

namespace IbanNet.DependencyInjection.Autofac.Specs;

public class ShouldResolveCustomService : TestHelpers.Specs.ShouldResolveCustomService
{
    public ShouldResolveCustomService() : base(new CustomServicesDependencyInjectionFixture())
    {
    }

    private class CustomServicesDependencyInjectionFixture
        : AutofacDependencyInjectionFixture
    {
        public CustomServicesDependencyInjectionFixture() : base(false)
        {
        }

        protected override void Configure(ContainerBuilder containerBuilder, Action<IIbanNetOptionsBuilder> configurer)
        {
            containerBuilder.RegisterInstance(Substitute.For<IIbanRegistry>());
            containerBuilder.RegisterInstance(Substitute.For<IIbanParser>());
            containerBuilder.RegisterInstance(Substitute.For<IIbanGenerator>());
            containerBuilder.RegisterInstance(Substitute.For<IIbanValidator>());

            base.Configure(containerBuilder, configurer);
        }
    }
}
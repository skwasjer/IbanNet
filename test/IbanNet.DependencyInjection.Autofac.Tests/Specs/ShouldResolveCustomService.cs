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
            containerBuilder.RegisterInstance(Mock.Of<IIbanRegistry>());
            containerBuilder.RegisterInstance(Mock.Of<IIbanParser>());
            containerBuilder.RegisterInstance(Mock.Of<IIbanGenerator>());
            containerBuilder.RegisterInstance(Mock.Of<IIbanValidator>());

            base.Configure(containerBuilder, configurer);
        }
    }
}
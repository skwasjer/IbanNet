using System;
using Autofac;
using TestHelpers.Fixtures;

namespace IbanNet.DependencyInjection.Autofac.Fixtures
{
    public class AutofacDependencyInjectionFixture : DependencyInjectionFixture<ContainerBuilder, IComponentContext>
    {
        private readonly bool _preserveStaticValidator;

        public AutofacDependencyInjectionFixture(bool preserveStaticValidator)
        {
            _preserveStaticValidator = preserveStaticValidator;
        }

        protected override ContainerBuilder CreateContainerBuilder()
        {
            return new();
        }

        protected override void Configure(ContainerBuilder containerBuilder, Action<IIbanNetOptionsBuilder> configurer)
        {
            containerBuilder.RegisterIbanNet(configurer, _preserveStaticValidator);
        }

        protected override IComponentContext CreateContainer(ContainerBuilder containerBuilder)
        {
            return containerBuilder.Build();
        }

        protected override DependencyResolverAdapter CreateAdapter(IComponentContext container)
        {
            return new AutofacDependencyResolverAdapter(container);
        }
    }
}

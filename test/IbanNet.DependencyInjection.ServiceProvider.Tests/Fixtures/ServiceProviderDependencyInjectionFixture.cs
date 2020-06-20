using System;
using Microsoft.Extensions.DependencyInjection;
using TestHelpers.Fixtures;

namespace IbanNet.DependencyInjection.ServiceProvider.Fixtures
{
    public class ServiceProviderDependencyInjectionFixture : DependencyInjectionFixture<IServiceCollection, IServiceProvider>
    {
        private readonly bool _preserveStaticValidator;

        public ServiceProviderDependencyInjectionFixture(bool preserveStaticValidator)
        {
            _preserveStaticValidator = preserveStaticValidator;
        }

        protected override IServiceCollection CreateContainerBuilder()
        {
            return new ServiceCollection();
        }

        protected override void Configure(IServiceCollection services, Action<IIbanNetOptionsBuilder> configurer)
        {
            services.AddIbanNet(configurer, _preserveStaticValidator);
        }

        protected override IServiceProvider CreateContainer(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }

        protected override DependencyResolverAdapter CreateAdapter(IServiceProvider container)
        {
            return new ServiceProviderDependencyResolverAdapter(container);
        }
    }
}

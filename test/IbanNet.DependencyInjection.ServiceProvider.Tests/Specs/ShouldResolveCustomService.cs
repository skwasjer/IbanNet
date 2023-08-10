using IbanNet.DependencyInjection.ServiceProvider.Fixtures;
using IbanNet.Registry;
using Microsoft.Extensions.DependencyInjection;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs;

public class ShouldResolveCustomService : TestHelpers.Specs.ShouldResolveCustomService
{
    public ShouldResolveCustomService() : base(new CustomServicesDependencyInjectionFixture())
    {
    }

    private class CustomServicesDependencyInjectionFixture
        : ServiceProviderDependencyInjectionFixture
    {
        public CustomServicesDependencyInjectionFixture() : base(false)
        {
        }

        protected override void Configure(IServiceCollection services, Action<IIbanNetOptionsBuilder> configurer)
        {
            services.AddSingleton(Substitute.For<IIbanRegistry>());
            services.AddSingleton(Substitute.For<IIbanParser>());
            services.AddSingleton(Substitute.For<IIbanGenerator>());
            services.AddSingleton(Substitute.For<IIbanValidator>());

            base.Configure(services, configurer);
        }
    }
}
using IbanNet.Validation.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace IbanNet.DependencyInjection.ServiceProvider
{
    internal class ServiceProviderDependencyResolverAdapter : DependencyResolverAdapter
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderDependencyResolverAdapter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public override object? GetService(Type serviceType)
        {
            object? instance = _serviceProvider.GetService(serviceType);
            if (instance is null && typeof(IIbanValidationRule).IsAssignableFrom(serviceType))
            {
                // Allow rules to be resolved without explicitly being registered.
                instance = ActivatorUtilities.CreateInstance(_serviceProvider, serviceType);
            }

            return instance;
        }
    }
}

using System.Reflection;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using IbanNet.Validation.Rules;

namespace IbanNet.DependencyInjection.Autofac;

/// <summary>
/// Registration source that handles automatic registration of <see cref="IIbanValidationRule" /> types.
/// </summary>
internal class RuleRegistrationSource : IRegistrationSource
{
    private static readonly TypeInfo IbanValidationRuleType = typeof(IIbanValidationRule).GetTypeInfo();

    public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
    {
        if (service is not IServiceWithType swt
         || !IbanValidationRuleType.IsAssignableFrom(swt.ServiceType.GetTypeInfo())
         || swt.ServiceType.GetTypeInfo().IsInterface)
        {
            // It's not a request for a rule type.
            return [];
        }

        // Return component registration, request per dependency, owned by lifetime scope.
#pragma warning disable CA2000 // Dispose objects before losing scope - justification: disposal is managed by Autofac
        var registration = new ComponentRegistration(
            Guid.NewGuid(),
            new ReflectionActivator(
                swt.ServiceType,
                new DefaultConstructorFinder(),
                new MostParametersConstructorSelector(),
                new List<Parameter>(),
                new List<Parameter>()
            ),
            new CurrentScopeLifetime(),
            InstanceSharing.None,
            InstanceOwnership.OwnedByLifetimeScope,
            [service],
            new Dictionary<string, object?>());
#pragma warning restore CA2000 // Dispose objects before losing scope

        return [registration];
    }

    public bool IsAdapterForIndividualComponents => false;
}

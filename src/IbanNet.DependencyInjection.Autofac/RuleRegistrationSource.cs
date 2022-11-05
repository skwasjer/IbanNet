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
            return Enumerable.Empty<IComponentRegistration>();
        }

        // Return component registration, request per dependency, owned by lifetime scope.
        var registration = new ComponentRegistration(
            Guid.NewGuid(),
#pragma warning disable CA2000 // Dispose objects before losing scope - justification: disposal is managed by Autofac
            new ReflectionActivator(
                swt.ServiceType,
                new DefaultConstructorFinder(),
                new MostParametersConstructorSelector(),
                new List<Parameter>(),
                new List<Parameter>()
            ),
#pragma warning restore CA2000 // Dispose objects before losing scope
            new CurrentScopeLifetime(),
            InstanceSharing.None,
            InstanceOwnership.OwnedByLifetimeScope,
            new[] { service },
            new Dictionary<string, object?>());

        return new IComponentRegistration[] { registration };
    }

    public bool IsAdapterForIndividualComponents => false;
}
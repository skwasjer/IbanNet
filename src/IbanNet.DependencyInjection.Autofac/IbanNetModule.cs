using Autofac;
using Autofac.Builder;
using Autofac.Core;
using IbanNet.Registry;

namespace IbanNet.DependencyInjection.Autofac;

internal class IbanNetModule : Module
{
    private readonly bool _preserveStaticValidator;
    private readonly List<Action<IActivatingEventArgs<IbanValidatorOptions>>> _ibanValidatorOptionsHandlers;

    public IbanNetModule(bool preserveStaticValidator)
    {
        _preserveStaticValidator = preserveStaticValidator;
        _ibanValidatorOptionsHandlers = new List<Action<IActivatingEventArgs<IbanValidatorOptions>>>();
    }

    public void OnActivated(Action<IActivatingEventArgs<IbanValidatorOptions>> handler)
    {
        _ibanValidatorOptionsHandlers.Add(handler);
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterSource(new RuleRegistrationSource());

        IRegistrationBuilder<IbanValidatorOptions, ConcreteReflectionActivatorData, SingleRegistrationStyle> optionsRegistration = builder
            .RegisterType<IbanValidatorOptions>()
            .AsSelf()
            .SingleInstance();

        foreach (Action<IActivatingEventArgs<IbanValidatorOptions>> handler in _ibanValidatorOptionsHandlers)
        {
            optionsRegistration.OnActivating(handler);
        }

        builder
            .RegisterType<AutofacDependencyResolverAdapter>()
            .As<DependencyResolverAdapter>()
            .IfNotRegistered(typeof(DependencyResolverAdapter))
            .SingleInstance();

        builder
            .Register(context => context.Resolve<IbanValidatorOptions>().Registry)
            .IfNotRegistered(typeof(IIbanRegistry))
            .SingleInstance();

        builder
            .RegisterType<IbanParser>()
            .UsingConstructor(() => new IbanParser((IIbanValidator)null!))
            .As<IIbanParser>()
            .IfNotRegistered(typeof(IIbanParser))
            .SingleInstance();

        builder
            .RegisterType<IbanGenerator>()
            .As<IIbanGenerator>()
            .IfNotRegistered(typeof(IIbanGenerator))
            .SingleInstance();

        builder
            .Register(context =>
            {
                IbanValidatorOptions options = context.Resolve<IbanValidatorOptions>();
                var validator = new IbanValidator(options);
                if (!_preserveStaticValidator)
                {
                    Iban.Validator = validator;
                }

                return validator;
            })
            .As<IIbanValidator>()
            .IfNotRegistered(typeof(IIbanValidator))
            .SingleInstance();
    }
}
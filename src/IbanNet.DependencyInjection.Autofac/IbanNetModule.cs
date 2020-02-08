using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace IbanNet.DependencyInjection.Autofac
{
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
				.InstancePerDependency();

			foreach (Action<IActivatingEventArgs<IbanValidatorOptions>> handler in _ibanValidatorOptionsHandlers)
			{
				optionsRegistration.OnActivating(handler);
			}

			builder
				.RegisterType<AutofacDependencyResolverAdapter>()
				.As<DependencyResolverAdapter>()
				.IfNotRegistered(typeof(DependencyResolverAdapter))
				.InstancePerDependency();

			builder
				.RegisterType<IbanParser>()
				.As<IIbanParser>()
				.IfNotRegistered(typeof(IIbanParser))
				.InstancePerDependency();

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
}

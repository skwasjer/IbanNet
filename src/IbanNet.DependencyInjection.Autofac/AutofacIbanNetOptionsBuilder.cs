using System;
using Autofac;

namespace IbanNet.DependencyInjection.Autofac
{
	internal class AutofacIbanNetOptionsBuilder : IIbanNetOptionsBuilder
	{
		private readonly IbanNetModule _module;

		internal AutofacIbanNetOptionsBuilder(IbanNetModule module)
		{
			_module = module ?? throw new ArgumentNullException(nameof(module));
		}

		public IIbanNetOptionsBuilder Configure(Action<DependencyResolverAdapter, IbanValidatorOptions> configure)
		{
			if (configure is null)
			{
				throw new ArgumentNullException(nameof(configure));
			}

			_module.OnActivated(args => configure(args.Context.Resolve<DependencyResolverAdapter>(), args.Instance));
			return this;
		}
	}
}

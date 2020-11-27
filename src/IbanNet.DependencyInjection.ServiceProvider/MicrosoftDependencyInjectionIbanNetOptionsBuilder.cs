using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IbanNet.DependencyInjection.ServiceProvider
{
    internal class MicrosoftDependencyInjectionIbanNetOptionsBuilder : IIbanNetOptionsBuilder
    {
        private readonly OptionsBuilder<IbanValidatorOptions> _validatorOptionsBuilder;

        internal MicrosoftDependencyInjectionIbanNetOptionsBuilder(IServiceCollection services)
        {
            _validatorOptionsBuilder = services.AddOptions<IbanValidatorOptions>();

#if NETSTANDARD2_1 || NET5_0
            _validatorOptionsBuilder.Validate(opts => opts.Registry is { }, "The 'Registry' is required.");
#endif
        }

        public IIbanNetOptionsBuilder Configure(Action<DependencyResolverAdapter, IbanValidatorOptions> configure)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            _validatorOptionsBuilder.Configure<DependencyResolverAdapter>(
                (options, adapter) => configure(adapter, options)
            );

            return this;
        }
    }
}

using System;
using System.ComponentModel;
using IbanNet.Registry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace IbanNet.DependencyInjection.ServiceProvider
{
    /// <summary>
    /// IbanNet dependency injection extensions.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers IbanNet services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection extended with IbanNet services.</returns>
        public static IServiceCollection AddIbanNet(this IServiceCollection services)
        {
            return services.AddIbanNet(false);
        }

        /// <summary>
        /// Registers IbanNet services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="preserveStaticValidator"><see langword="true" /> to preserve the static validator in <see cref="Iban.Validator" />, or <see langword="false" /> to replace with the configured instance.</param>
        /// <returns>The service collection extended with IbanNet services.</returns>
        public static IServiceCollection AddIbanNet(this IServiceCollection services, bool preserveStaticValidator)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            services.TryAddTransient<DependencyResolverAdapter, ServiceProviderDependencyResolverAdapter>();

            services.TryAddTransient<IIbanParser, IbanParser>();
            services.TryAddSingleton<IIbanValidator>(s =>
            {
                IbanValidatorOptions options = s.GetRequiredService<IOptions<IbanValidatorOptions>>().Value;
                var validator = new IbanValidator(options);
                if (!preserveStaticValidator)
                {
                    Iban.Validator = validator;
                }

                return validator;
            });
            services.TryAddTransient<IIbanGenerator>(s =>
            {
                IbanValidatorOptions options = s.GetRequiredService<IOptions<IbanValidatorOptions>>().Value;
                return new IbanGenerator(options.Registry);
            });

            return services;
        }

        /// <summary>
        /// Registers IbanNet services using specified options builder.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configure">The options builder.</param>
        /// <returns>The service collection extended with IbanNet services.</returns>
        public static IServiceCollection AddIbanNet(this IServiceCollection services, Action<IIbanNetOptionsBuilder> configure)
        {
            return services.AddIbanNet(configure, false);
        }

        /// <summary>
        /// Registers IbanNet services using specified options builder.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configure">The options builder.</param>
        /// <param name="preserveStaticValidator"><see langword="true" /> to preserve the static validator in <see cref="Iban.Validator" />, or <see langword="false" /> to replace with the configured instance.</param>
        /// <returns>The service collection extended with IbanNet services.</returns>
        public static IServiceCollection AddIbanNet(this IServiceCollection services, Action<IIbanNetOptionsBuilder> configure, bool preserveStaticValidator)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            services.AddIbanNet(preserveStaticValidator);

            var builder = new MicrosoftDependencyInjectionIbanNetOptionsBuilder(services);
            configure(builder);

            return services;
        }
    }
}

using System.ComponentModel;
using Autofac;

namespace IbanNet.DependencyInjection.Autofac
{
    /// <summary>
    /// IbanNet dependency injection extensions.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AutofacRegistrationExtensions
    {
        /// <summary>
        /// Registers IbanNet services.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        /// <param name="preserveStaticValidator"><see langword="true" /> to preserve the static validator in <see cref="Iban.Validator" />, or <see langword="false" /> to replace with the configured instance.</param>
        /// <returns>The container builder so that additional calls can be chained.</returns>
        public static ContainerBuilder RegisterIbanNet(this ContainerBuilder containerBuilder, bool preserveStaticValidator = false)
        {
            return RegisterIbanNet(containerBuilder, _ => { }, preserveStaticValidator);
        }

        /// <summary>
        /// Registers IbanNet services using specified options builder.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        /// <param name="configure">The options builder.</param>
        /// <param name="preserveStaticValidator"><see langword="true" /> to preserve the static validator in <see cref="Iban.Validator" />, or <see langword="false" /> to replace with the configured instance.</param>
        /// <returns>The container builder so that additional calls can be chained.</returns>
        public static ContainerBuilder RegisterIbanNet(this ContainerBuilder containerBuilder, Action<IIbanNetOptionsBuilder> configure, bool preserveStaticValidator = false)
        {
            if (containerBuilder is null)
            {
                throw new ArgumentNullException(nameof(containerBuilder));
            }

            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var module = new IbanNetModule(preserveStaticValidator);
            var builder = new AutofacIbanNetOptionsBuilder(module);
            configure(builder);
            containerBuilder.RegisterModule(module);
            return containerBuilder;
        }
    }
}

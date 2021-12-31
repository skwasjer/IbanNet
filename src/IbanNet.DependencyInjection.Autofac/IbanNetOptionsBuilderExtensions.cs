using System.ComponentModel;
using Autofac;
using IbanNet.Validation.Rules;

namespace IbanNet.DependencyInjection.Autofac
{
    /// <summary>
    /// Extensions for <see cref="IIbanNetOptionsBuilder" />.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IbanNetOptionsBuilderExtensions
    {
        /// <summary>
        /// Registers a handler to configure the options when the builder executes.
        /// </summary>
        /// <param name="builder">The builder instance.</param>
        /// <param name="configure">The handler that is called when configuring the options.</param>
        /// <returns>The <see cref="IIbanNetOptionsBuilder" /> so that additional calls can be chained.</returns>
        public static IIbanNetOptionsBuilder Configure(this IIbanNetOptionsBuilder builder, Action<IComponentContext, IbanValidatorOptions> configure)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            return builder.Configure(
                (adapter, options) => configure(adapter.GetRequiredService<IComponentContext>(), options)
            );
        }

        /// <summary>
        /// Registers a custom validation rule that is executed after built-in validation has passed.
        /// </summary>
        /// <typeparam name="T">The type of the validation rule.</typeparam>
        /// <param name="builder">The builder instance.</param>
        /// <param name="implementationFactory">The factory returning a new instance of the rule.</param>
        /// <returns>The <see cref="IIbanNetOptionsBuilder" /> so that additional calls can be chained.</returns>
        public static IIbanNetOptionsBuilder WithRule<T>(this IIbanNetOptionsBuilder builder, Func<IComponentContext, T> implementationFactory)
            where T : class, IIbanValidationRule
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (implementationFactory is null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return builder.Configure((context, options) => options.Rules.Add(
                    implementationFactory(context.Resolve<IComponentContext>())
                )
            );
        }
    }
}

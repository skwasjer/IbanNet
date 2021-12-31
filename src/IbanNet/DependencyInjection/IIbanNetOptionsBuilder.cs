using System.ComponentModel;

namespace IbanNet.DependencyInjection
{
    /// <summary>
    /// Describes the options builder for IbanNet, that can be implemented by dependency injection frameworks.
    /// </summary>
    public interface IIbanNetOptionsBuilder : IFluentInterface
    {
        /// <summary>
        /// Registers a handler to configure the options when the builder executes.
        /// </summary>
        /// <param name="configure">The handler that is called when configuring the options.</param>
        /// <returns>The <see cref="IIbanNetOptionsBuilder" /> so that additional calls can be chained.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IIbanNetOptionsBuilder Configure(Action<DependencyResolverAdapter, IbanValidatorOptions> configure);
    }
}

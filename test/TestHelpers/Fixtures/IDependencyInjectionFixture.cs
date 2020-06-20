using System;
using IbanNet.DependencyInjection;

namespace TestHelpers.Fixtures
{
    public interface IDependencyInjectionFixture
    {
        /// <summary>
        /// Configures an IbanNet named instance using specified <paramref name="configurer" />.
        /// </summary>
        void Configure(Action<IIbanNetOptionsBuilder> configurer);

        /// <summary>
        /// Builds the container and returns an adapter.
        /// </summary>
        DependencyResolverAdapter Build();
    }
}

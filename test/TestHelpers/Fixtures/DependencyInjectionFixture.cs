using IbanNet.DependencyInjection;

namespace TestHelpers.Fixtures;

/// <summary>
/// Base fixture for testing IbanNet registrations.
/// </summary>
/// <typeparam name="TContainerBuilder"></typeparam>
/// <typeparam name="TContainer"></typeparam>
public abstract class DependencyInjectionFixture<TContainerBuilder, TContainer> : IDependencyInjectionFixture
{
    private readonly TContainerBuilder _containerBuilder;

    protected DependencyInjectionFixture()
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        //   - justification: we accept this, just have to consider this when implementing DI fixtures.
        _containerBuilder = CreateContainerBuilder();
    }

    /// <summary>
    /// Creates the DI container builder.
    /// </summary>
    protected abstract TContainerBuilder CreateContainerBuilder();

    /// <summary>
    /// Creates the DI container from the provided <paramref name="containerBuilder" />.
    /// </summary>
    protected abstract TContainer CreateContainer(TContainerBuilder containerBuilder);

    /// <summary>
    /// Creates an adapter around specified DI <paramref name="container" />.
    /// </summary>
    protected abstract DependencyResolverAdapter CreateAdapter(TContainer container);

    /// <summary>
    /// Configures the container with IbanNet using specified <paramref name="configurer" />.
    /// </summary>
    protected abstract void Configure(TContainerBuilder containerBuilder, Action<IIbanNetOptionsBuilder> configurer);

    /// <summary>
    /// Configures IbanNet using specified <paramref name="configurer" />.
    /// </summary>
    public void Configure(Action<IIbanNetOptionsBuilder> configurer)
    {
        Configure(_containerBuilder, configurer);
    }

    public DependencyResolverAdapter Build()
    {
        return CreateAdapter(CreateContainer(_containerBuilder));
    }
}
namespace IbanNet.DependencyInjection
{
    /// <summary>
    /// A base adapter for dependency injection implementations to implement.
    /// </summary>
    public abstract class DependencyResolverAdapter
    {
        /// <summary>
        /// Gets a service by <paramref name="serviceType" />.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>An instance of the <paramref name="serviceType" />.</returns>
        public abstract object? GetService(Type serviceType);

        /// <summary>
        /// Gets a service by <paramref name="serviceType" />.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>An instance of the <paramref name="serviceType" />.</returns>
        public object GetRequiredService(Type serviceType)
        {
            object? service = GetService(serviceType);
            if (service is null)
            {
                throw new InvalidOperationException($"Failed to resolve {serviceType}.");
            }

            return service;
        }

        /// <summary>
        /// Gets a service of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <returns>An instance of type <typeparamref name="T" />.</returns>
        public T GetRequiredService<T>()
            where T : class
        {
            return (T)GetRequiredService(typeof(T));
        }

        /// <summary>
        /// Gets a service of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <returns>An instance of type <typeparamref name="T" />.</returns>
        public T? GetService<T>()
            where T : class
        {
            return GetService(typeof(T)) as T;
        }
    }
}

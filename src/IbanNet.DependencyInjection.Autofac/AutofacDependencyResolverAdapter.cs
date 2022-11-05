using Autofac;

namespace IbanNet.DependencyInjection.Autofac;

internal class AutofacDependencyResolverAdapter : DependencyResolverAdapter
{
    private readonly IComponentContext _componentContext;

    public AutofacDependencyResolverAdapter(IComponentContext componentContext)
    {
        _componentContext = componentContext ?? throw new ArgumentNullException(nameof(componentContext));
    }

    public override object GetService(Type serviceType)
    {
        return _componentContext.Resolve(serviceType);
    }
}
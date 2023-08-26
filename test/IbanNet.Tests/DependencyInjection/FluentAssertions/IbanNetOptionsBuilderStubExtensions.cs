namespace IbanNet.DependencyInjection.FluentAssertions;

public static class IbanNetOptionsBuilderStubExtensions
{
    public static IbanNetOptionsBuilderStubAssertions<T> Should<T>(this T instance)
        where T : class, IIbanNetOptionsBuilder
    {
        return new(instance);
    }
}
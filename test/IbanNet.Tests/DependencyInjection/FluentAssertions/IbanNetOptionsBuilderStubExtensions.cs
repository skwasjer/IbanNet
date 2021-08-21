using Moq;

namespace IbanNet.DependencyInjection.FluentAssertions
{
    public static class IbanNetOptionsBuilderStubExtensions
    {
        public static IbanNetOptionsBuilderStubAssertions<T> Should<T>(this Mock<T> instance)
            where T : class, IIbanNetOptionsBuilder
        {
            return new(instance);
        }
    }
}

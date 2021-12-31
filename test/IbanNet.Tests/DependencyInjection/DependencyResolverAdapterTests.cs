using TestHelpers;
using TestHelpers.FluentAssertions;

namespace IbanNet.DependencyInjection
{
    public class DependencyResolverAdapterTests
    {
        private readonly DependencyResolverAdapter _sut;
        private readonly Mock<DependencyResolverAdapter> _adapterStub;

        private class TestService
        {
        }

        public DependencyResolverAdapterTests()
        {
            _sut = CreateAdapterStub();
            _adapterStub = Mock.Get(_sut);
        }

        private static DependencyResolverAdapter CreateAdapterStub()
        {
            var adapterStub = new Mock<DependencyResolverAdapter> { CallBase = true };
            adapterStub
                .Setup(m => m.GetService(It.IsAny<Type>()))
                .Returns<Type>(Activator.CreateInstance);
            return adapterStub.Object;
        }

        [Fact]
        public void Given_service_is_not_registered_when_getting_generic_required_it_should_throw()
        {
            _adapterStub
                .Setup(m => m.GetService(It.IsAny<Type>()))
                .Returns(null)
                .Verifiable();

            // Act
            Action act = () => _sut.GetRequiredService<TestService>();

            // Assert
            act.Should().Throw<InvalidOperationException>();
            _adapterStub.Verify();
        }

        [Fact]
        public void Given_service_is_not_registered_when_getting_required_it_should_throw()
        {
            _adapterStub
                .Setup(m => m.GetService(It.IsAny<Type>()))
                .Returns(null)
                .Verifiable();

            // Act
            Action act = () => _sut.GetRequiredService(typeof(TestService));

            // Assert
            act.Should().Throw<InvalidOperationException>();
            _adapterStub.Verify();
        }

        [Theory]
        [MemberData(nameof(ResolvesSuccessfullyTestCases))]
        public void Given_service_is_registered_when_resolving_it_should_return_service(params object[] args)
        {
            var act = (Delegate)args[0];
            act.Should().NotThrow(args.Skip(1).ToArray()).Subject.Should().BeOfType<TestService>();
        }

        public static IEnumerable<object[]> ResolvesSuccessfullyTestCases()
        {
            DependencyResolverAdapter instance = CreateAdapterStub();
            yield return DelegateTestCase.Create(instance.GetService, typeof(TestService)).WithoutName();
            yield return DelegateTestCase.Create(instance.GetRequiredService, typeof(TestService)).WithoutName();
            yield return DelegateTestCase.Create(instance.GetService<TestService>).WithoutName();
            yield return DelegateTestCase.Create(instance.GetRequiredService<TestService>).WithoutName();
        }
    }
}

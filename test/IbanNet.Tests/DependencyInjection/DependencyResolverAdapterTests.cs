using TestHelpers;
using TestHelpers.FluentAssertions;

namespace IbanNet.DependencyInjection;

public class DependencyResolverAdapterTests
{
    private readonly DependencyResolverAdapter _sut;

    private class TestService
    {
    }

    public DependencyResolverAdapterTests()
    {
        _sut = CreateAdapterStub();
    }

    private static DependencyResolverAdapter CreateAdapterStub()
    {
        DependencyResolverAdapter adapterStub = Substitute.For<DependencyResolverAdapter>();
        adapterStub
            .GetService(Arg.Any<Type>())
            .Returns(info => Activator.CreateInstance(info.Arg<Type>()));
        return adapterStub;
    }

    [Fact]
    public void Given_service_is_not_registered_when_getting_generic_required_it_should_throw()
    {
        _sut
            .GetService(Arg.Any<Type>())
            .Returns(null);

        // Act
        Action act = () => _sut.GetRequiredService<TestService>();

        // Assert
        act.Should().Throw<InvalidOperationException>();
        _sut.Received(1).GetService(Arg.Any<Type>());
    }

    [Fact]
    public void Given_service_is_not_registered_when_getting_required_it_should_throw()
    {
        _sut
            .GetService(Arg.Any<Type>())
            .Returns(null);

        // Act
        Action act = () => _sut.GetRequiredService(typeof(TestService));

        // Assert
        act.Should().Throw<InvalidOperationException>();
        _sut.Received(1).GetService(Arg.Any<Type>());
    }

    [Theory]
    [MemberData(nameof(ResolvesSuccessfullyTestCases))]
    public void Given_service_is_registered_when_resolving_it_should_return_service(params object[] args)
    {
        var act = (Delegate)args[0];
        act.Should().NotThrow(args.Skip(1).ToArray()).Subject.Should().BeOfType<TestService>();
    }

    public static IEnumerable<object?[]> ResolvesSuccessfullyTestCases()
    {
        DependencyResolverAdapter instance = CreateAdapterStub();
        yield return DelegateTestCase.Create(instance.GetService, typeof(TestService)).WithoutName();
        yield return DelegateTestCase.Create(instance.GetRequiredService, typeof(TestService)).WithoutName();
        yield return DelegateTestCase.Create(instance.GetService<TestService>).WithoutName();
        yield return DelegateTestCase.Create(instance.GetRequiredService<TestService>).WithoutName();
    }
}

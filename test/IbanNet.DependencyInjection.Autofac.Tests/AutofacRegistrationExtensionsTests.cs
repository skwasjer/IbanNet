using Autofac;

namespace IbanNet.DependencyInjection.Autofac;

public class AutofacRegistrationExtensionsTests
{
    [Fact]
    public void Given_null_argument_when_registering_it_should_throw()
    {
        ContainerBuilder? containerBuilder = null;

        // Act
        Func<ContainerBuilder> act = () => containerBuilder!.RegisterIbanNet();

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(containerBuilder));
    }

    public static IEnumerable<object?[]> RegisterIbanNet_InvalidArgumentTestCases()
    {
        var containerBuilder = new ContainerBuilder();
        // ReSharper disable once ConvertToLocalFunction
        Action<object> configure = _ => { };

        yield return new object?[] { null, configure, nameof(containerBuilder) };
        yield return new object?[] { containerBuilder, null, nameof(configure) };
    }

    [Theory]
    [MemberData(nameof(RegisterIbanNet_InvalidArgumentTestCases))]
    public void Given_null_argument_when_registering_with_configure_it_should_throw(ContainerBuilder containerBuilder, Action<object> configure, string expectedParamName)
    {
        // Act
        Action act = () => containerBuilder.RegisterIbanNet(configure);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(expectedParamName);
    }
}

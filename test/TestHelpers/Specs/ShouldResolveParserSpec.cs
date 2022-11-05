using IbanNet;
using TestHelpers.Fixtures;

namespace TestHelpers.Specs;

public abstract class ShouldResolveParserSpec : DiSpec
{
    protected ShouldResolveParserSpec(IDependencyInjectionFixture fixture) : base(fixture)
    {
    }

    protected override void Given()
    {
        Fixture.Configure(builder => { });
    }

    [Fact]
    public void When_resolving_parser_it_should_not_throw()
    {
        // Assert
        Func<IIbanParser> act = () => Subject.GetService<IIbanParser>();

        // Act
        act.Should().NotThrow().Which.Should().NotBeNull();
    }

    [Fact]
    public void When_resolving_twice_it_should_return_same_instance()
    {
        // Assert
        IIbanParser first = Subject.GetService<IIbanParser>();
        IIbanParser second = Subject.GetService<IIbanParser>();

        // Act
        first.Should().BeSameAs(second);
    }
}
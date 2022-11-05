using IbanNet.Registry.Patterns;

namespace IbanNet.Registry;

public class IbanStructureTests
{
    [Fact]
    public void When_creating_with_null_pattern_it_should_throw()
    {
        Pattern? pattern = null;

        // Act
        Func<IbanStructure> act = () => new IbanStructure(pattern!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be(nameof(pattern));
    }
}

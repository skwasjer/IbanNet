using IbanNet.Registry.Patterns;

namespace IbanNet.Registry;

public class BankStructureTests
{
    [Fact]
    public void When_creating_with_null_pattern_it_should_throw()
    {
        Pattern pattern = null;

        // Act
        // ReSharper disable once AssignNullToNotNullAttribute
        Func<BankStructure> act = () => new BankStructure(pattern);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be(nameof(pattern));
    }
}
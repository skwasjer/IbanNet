using IbanNet.Registry.Patterns;

namespace IbanNet.Registry
{
    public class BranchStructureTests
    {
        [Fact]
        public void When_creating_with_null_pattern_it_should_throw()
        {
            Pattern pattern = null;

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            Func<BranchStructure> act = () => new BranchStructure(pattern);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(pattern));
        }
    }
}

using IbanNet.Registry.Patterns;
using IbanNet.Registry.Swift;

namespace IbanNet.Registry;

public class StructureSectionTests
{
    private class TestStructureSection : StructureSection
    {
        public TestStructureSection(Pattern pattern, int position = 0) : base(pattern, position)
        {
        }
    }

    [Fact]
    public void When_creating_structureSection_it_should_initialize_properties()
    {
        Pattern pattern = NullPattern.Instance;

        // Act
        StructureSection structure = new TestStructureSection(pattern);

        // Assert
        structure.Pattern.Should().BeSameAs(pattern);
        structure.Example.Should().BeEmpty();
        structure.Length.Should().Be(0);
        structure.Position.Should().Be(0);
    }

    [Fact]
    public void When_creating_structureSection_with_null_structure_it_should_throw()
    {
        Pattern pattern = null;

        // Act
        // ReSharper disable once ExpressionIsAlwaysNull
        Func<TestStructureSection> act = () => new TestStructureSection(pattern);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be(nameof(pattern));
    }

    [Fact]
    public void When_setting_example_to_null_it_should_set_to_empty_string()
    {
        // Act
        // ReSharper disable once AssignNullToNotNullAttribute
        var sut = new TestStructureSection(NullPattern.Instance) { Example = null };

        // Assert
        sut.Example.Should().BeEmpty();
    }

    [Fact]
    public void When_creating_structureSection_with_pattern_it_should_set_properties()
    {
        var pattern = new SwiftPattern("2!n");
        const int position = 12;

        // Act
        StructureSection structure = new TestStructureSection(pattern, position);

        // Assert
        structure.Pattern.Should().BeSameAs(pattern);
        structure.Length.Should().Be(2);
        structure.Position.Should().Be(position);
    }

    [Fact]
    public void When_creating_structureSection_with_null_pattern_it_should_throw()
    {
        Pattern pattern = null;

        // Act
        // ReSharper disable once ExpressionIsAlwaysNull
        // ReSharper disable once ObjectCreationAsStatement
        Func<TestStructureSection> act = () => new TestStructureSection(pattern, 0);

        // Assert
        act.Should()
            .ThrowExactly<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be(nameof(pattern));
    }

    [Fact]
    public void When_creating_structureSection_with_invalid_position_it_should_throw()
    {
        const int position = -1;

        // Act
        // ReSharper disable once ObjectCreationAsStatement
        Func<TestStructureSection> act = () => new TestStructureSection(new SwiftPattern("2!n"), position);

        // Assert
        act.Should()
            .ThrowExactly<ArgumentOutOfRangeException>()
            .Which.ParamName.Should()
            .Be(nameof(position));
    }
}
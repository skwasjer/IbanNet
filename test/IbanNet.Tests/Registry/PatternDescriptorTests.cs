using IbanNet.Registry.Patterns;
using IbanNet.Registry.Swift;

namespace IbanNet.Registry;

public class PatternDescriptorTests
{
    [Fact]
    public void When_creating_patternDescriptor_with_defaults_it_should_initialize_properties()
    {
        Pattern pattern = NullPattern.Instance;

        // Act
        var sut = new PatternDescriptor(pattern);

        // Assert
        sut.Pattern.Should().BeSameAs(pattern);
        sut.Example.Should().BeEmpty();
        sut.Length.Should().Be(0);
        sut.Position.Should().Be(0);
    }

    [Fact]
    public void When_creating_patternDescriptor_with_null_pattern_it_should_throw()
    {
        Pattern? pattern = null;

        // Act
        Func<PatternDescriptor> act = () => new PatternDescriptor(pattern!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(pattern));
    }

    [Fact]
    public void When_setting_example_to_null_it_should_set_to_empty_string()
    {
        // Act
        var sut = new PatternDescriptor(NullPattern.Instance) { Example = null };

        // Assert
        sut.Example.Should().BeEmpty();
    }

    [Fact]
    public void When_setting_example_it_should_set_property()
    {
        // Act
        var sut = new PatternDescriptor(NullPattern.Instance) { Example = "1234ABC" };

        // Assert
        sut.Example.Should().Be("1234ABC");
    }

    [Fact]
    public void When_creating_patternDescriptor_with_pattern_it_should_set_properties()
    {
        var pattern = new TestPattern([new PatternToken(AsciiCategory.Digit, 2)]);

        // Act
        var sut = new PatternDescriptor(pattern);

        // Assert
        sut.Pattern.Should().BeSameAs(pattern);
        sut.Length.Should().Be(2);
    }

    [Fact]
    public void When_creating_patternDescriptor_with_invalid_position_it_should_throw()
    {
        const int position = -1;

        // Act
        Func<PatternDescriptor> act = () => new PatternDescriptor(new TestPattern([new PatternToken(AsciiCategory.Digit, 2)]), position);

        // Assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithParameterName(nameof(position));
    }

    [Fact]
    public void When_creating_patternDescriptor_with_position_it_should_set_property()
    {
        var pattern = new TestPattern([new PatternToken(AsciiCategory.Digit, 2)]);
        const int position = 12;

        // Act
        var sut = new PatternDescriptor(pattern, position);

        // Assert
        sut.Position.Should().Be(position);
    }
}

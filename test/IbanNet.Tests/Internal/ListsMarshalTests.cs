#if NET6_0 || NET8_0_OR_GREATER
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IbanNet.Internal;

public sealed class ListsMarshalTests
{
    [Fact]
    public void Given_that_input_is_null_when_getting_span_it_should_return_default()
    {
        Span<int> actual = ListsMarshal.AsSpan((List<int>)null!);

        // Assert
        actual.Length.Should().Be(0);
    }

    [Fact]
    public void Given_that_input_is_empty_when_getting_span_it_should_return_default()
    {
        Span<int> actual = ListsMarshal.AsSpan(new List<int>());

        // Assert
        actual.Length.Should().Be(0);
    }

    [Fact]
    public void Given_that_input_is_array_when_getting_span_it_should_return_span_wrapping_the_array()
    {
        int[] arr = [1, 2, 3];

        // Act
        Span<int> actual = ListsMarshal.AsSpan(arr);

        // Assert
        actual.Length.Should().Be(3);
        actual[0].Should().Be(1);
        actual[1].Should().Be(2);
        actual[2].Should().Be(3);

        ref int src = ref MemoryMarshal.GetArrayDataReference(arr);
        ref int dest = ref actual.GetPinnableReference();
        Unsafe.AreSame(ref src, ref dest).Should().BeTrue();
    }

    [Fact]
    public void Given_that_input_is_list_when_getting_span_it_should_return_span_wrapping_the_list()
    {
        List<int> list = [4, 5, 6];

        // Act
        Span<int> actual = ListsMarshal.AsSpan(list);

        // Assert
        actual.Length.Should().Be(3);
        actual[0].Should().Be(4);
        actual[1].Should().Be(5);
        actual[2].Should().Be(6);

        ref int src = ref CollectionsMarshal.AsSpan(list).GetPinnableReference();
        ref int dest = ref actual.GetPinnableReference();
        Unsafe.AreSame(ref src, ref dest).Should().BeTrue();
    }

    [Fact]
    public void Given_that_input_is_single_element_not_in_a_list_or_array_when_getting_span_it_should_return_single_element_span()
    {
        var readOnlyList = new ReadOnlyCollection<int>((int[])[1]);

        // Act
        Span<int> actual = ListsMarshal.AsSpan(readOnlyList);

        // Assert
        actual.Length.Should().Be(1);
        actual[0].Should().Be(1);
    }

    [Fact]
    public void Given_that_input_is_not_a_list_or_array_when_getting_span_it_should_return_span_as_copy()
    {
        var list = new ReadOnlyCollection<int>((int[])[7, 8, 9]);

        // Act
        Span<int> actual = ListsMarshal.AsSpan(list);

        // Assert
        actual.Length.Should().Be(3);
        actual[0].Should().Be(7);
        actual[1].Should().Be(8);
        actual[2].Should().Be(9);
    }
}
#endif

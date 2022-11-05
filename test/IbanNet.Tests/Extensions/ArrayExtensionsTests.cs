namespace IbanNet.Extensions;

public class ArrayExtensionsTests
{
    public class FillTests : ArrayExtensionsTests
    {
        [Fact]
        public void Given_array_when_filling_it_should_not_throw_and_return_null()
        {
            int[] array = { 1, 2, 3 };
            int[] expectedArray = { 4, 4, 4 };

            // Act
            int[] actual = array.Fill(4);

            // Assert
            actual.Should().BeEquivalentTo(expectedArray);
        }
    }
}
using System.Globalization;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Wikipedia
{
    public class WikipediaPatternTokenizerTests
    {
        private readonly WikipediaPatternTokenizer _sut;

        public WikipediaPatternTokenizerTests()
        {
            _sut = new WikipediaPatternTokenizer();
        }

#if !USE_SPANS
        [Fact]
        public void Given_null_input_when_tokenizing_it_should_throw()
        {
            char[] input = null;

            // Act
            // ReSharper disable once IteratorMethodResultIsIgnored
            // ReSharper disable once AssignNullToNotNullAttribute
            Action act = () => _sut.Tokenize(input);

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(input));
        }
#endif

        [Theory]
        [InlineData("2n", "12", true)]
        [InlineData("3n", "1234", false)]
        [InlineData("2n", "1A", false)]
        [InlineData("2n", "", false)]
        [InlineData("2n", "1", false)]
        [InlineData("2n", "123", false)]
        [InlineData("8n6a", "AB", false)]
        [InlineData("1a", "A", true)]
        [InlineData("1a1n", "A1", true)]
        [InlineData("3c", "d1F", true)]
        [InlineData("2n", "@#", false)]
        [InlineData("2n3a2c", "12ABCe1", true)]
        [InlineData("2n3a2c", "12123e1", false)]
        [InlineData("2n3a3c", "", false)]
        [InlineData("2n3a", "12ABCD", false)]
        public void Given_valid_pattern_without_countryCode_it_should_decompose_into_tests(string pattern, string value, bool expectedResult)
        {
            // Act
            var validator = new PatternValidator(new FakePattern(_sut.Tokenize(pattern)));
            bool result = validator.Validate(value);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("A", "A", 0)]
        [InlineData("2z", "2z", 0)]
        [InlineData("2a2!n", "2!n", 1)]
        public void Given_invalid_pattern_when_tokenizing_it_should_throw(string pattern, string token, int pos)
        {
            // Act
            Action act = () => _sut.Tokenize(pattern);

            // Assert
            act.Should()
                .ThrowExactly<PatternException>()
                .WithMessage(string.Format(CultureInfo.CurrentCulture, Resources.ArgumentException_The_structure_segment_0_is_invalid, token, pos) + "*");
        }
    }
}

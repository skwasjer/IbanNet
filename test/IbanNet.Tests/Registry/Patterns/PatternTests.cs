namespace IbanNet.Registry.Patterns;

public class PatternTests
{
    private readonly Mock<FakePatternTokenizer> _tokenizerMock;

    protected PatternTests()
    {
        _tokenizerMock = new Mock<FakePatternTokenizer>();
    }

    public class Given_a_pattern_created_with_tokens : PatternTests
    {
        private readonly List<PatternToken> _tokens;

        public Given_a_pattern_created_with_tokens()
        {
            _tokens = new List<PatternToken>
            {
                new(AsciiCategory.AlphaNumeric, 3),
                new(AsciiCategory.Digit, 2)
            };
        }

        [Fact]
        public void When_creating_with_null_pattern_it_should_throw()
        {
            IEnumerable<PatternToken>? tokens = null;

            // Act
            Func<FakePattern> act = () => new FakePattern(tokens!);

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(tokens));
        }

        [Fact]
        public void When_getting_tokens_it_should_return_expected()
        {
            // Act
            var sut = new FakePattern(_tokens);

            // Assert
            sut.Tokens.Should()
                .BeAssignableTo<IReadOnlyCollection<PatternToken>>()
                .And.BeEquivalentTo(_tokens);
            _tokenizerMock.Verify(m => m.Tokenize(It.IsAny<IEnumerable<char>>()), Times.Never);
        }

        [Fact]
        public void When_getting_string_representation_it_should_return_expected()
        {
            // Act
            var sut = new FakePattern(_tokens);

            // Assert
            sut.ToString().Should().Be("AlphaNumeric[3],Digit[2]");
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        public void Given_that_at_least_one_token_is_not_fixed_length_when_getting_pattern_fixed_length_it_should_return_false(int nonFixedTokensToAdd, bool shouldBeFixedLength)
        {
            _tokens.AddRange(Enumerable.Repeat(new PatternToken(AsciiCategory.UppercaseLetter, 1, 3), nonFixedTokensToAdd));

            new FakePattern(_tokens).IsFixedLength.Should().Be(shouldBeFixedLength);
        }
    }

    public class Given_a_pattern_created_with_string_pattern_and_tokenizer : PatternTests
    {
        private const string TestPattern = "C3N2";
        private readonly List<PatternToken> _tokens;

        public Given_a_pattern_created_with_string_pattern_and_tokenizer()
        {
            _tokens = new List<PatternToken>
            {
                new(AsciiCategory.AlphaNumeric, 3),
                new(AsciiCategory.Digit, 2)
            };

            _tokenizerMock
                .Setup(m => m.Tokenize((IEnumerable<char>)TestPattern))
                .Returns(_tokens)
                .Verifiable();
        }

        [Fact]
        public void When_creating_with_null_pattern_it_should_throw()
        {
            string? pattern = null;

            // Act
            Func<FakePattern> act = () => new FakePattern(pattern!, Mock.Of<ITokenizer<PatternToken>>());

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(pattern));
        }

        [Fact]
        public void When_creating_with_null_tokenizer_it_should_throw()
        {
            ITokenizer<PatternToken>? tokenizer = null;

            // Act
            Func<FakePattern> act = () => new FakePattern(string.Empty, tokenizer!);

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(tokenizer));
        }

        [Fact]
        public void When_getting_tokens_it_should_return_expected()
        {
            // Act
            var sut = new FakePattern(TestPattern, _tokenizerMock.Object);

            // Assert
            sut.Tokens.Should()
                .BeAssignableTo<IReadOnlyCollection<PatternToken>>()
                .And.BeEquivalentTo(_tokens);
            _tokenizerMock.Verify();
        }

        [Fact]
        public void When_getting_tokens_multiple_times_it_should_only_tokenize_once()
        {
            // Act
            var sut = new FakePattern(TestPattern, _tokenizerMock.Object);

            // Assert
            sut.Tokens.Should().BeEquivalentTo(_tokens);
            sut.Tokens.Should().HaveCount(2);
            _tokenizerMock.Verify(m => m.Tokenize(It.IsAny<IEnumerable<char>>()), Times.Once);
        }

        [Fact]
        public void When_getting_string_representation_it_should_return_expected()
        {
            // Act
            var sut = new FakePattern(TestPattern, _tokenizerMock.Object);

            // Assert
            sut.ToString().Should().Be(TestPattern);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        public void Given_that_at_least_one_token_is_not_fixed_length_when_getting_pattern_fixed_length_it_should_return_false(int nonFixedTokensToAdd, bool shouldBeFixedLength)
        {
            _tokens.AddRange(Enumerable.Repeat(new PatternToken(AsciiCategory.UppercaseLetter, 1, 3), nonFixedTokensToAdd));

            new FakePattern(TestPattern, _tokenizerMock.Object).IsFixedLength.Should().Be(shouldBeFixedLength);
        }
    }

    internal class FakePatternTokenizer : ITokenizer<PatternToken>
    {
#if USE_SPANS
        public IEnumerable<PatternToken> Tokenize(ReadOnlySpan<char> input)
        {
            return Tokenize((IEnumerable<char>)input.ToArray());
        }
#endif
        public virtual IEnumerable<PatternToken> Tokenize(IEnumerable<char> input)
        {
            throw new NotImplementedException();
        }
    }
}

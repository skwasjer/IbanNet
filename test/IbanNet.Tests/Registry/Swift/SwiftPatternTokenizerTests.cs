using System;
using System.Globalization;
using FluentAssertions;
using IbanNet.Registry.Patterns;
using IbanNet.Validation;
using Xunit;

namespace IbanNet.Registry.Swift
{
    public class SwiftPatternTokenizerTests
    {
        private readonly SwiftPatternTokenizer _sut;

        public SwiftPatternTokenizerTests()
        {
            _sut = new SwiftPatternTokenizer();
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
        [InlineData("2!n", "12", true)]
        [InlineData("3!n", "1234", false)]
        [InlineData("2!n", "1A", false)]
        [InlineData("2n", "", false)]
        [InlineData("2n", "1", true)]
        [InlineData("2n", "12", true)]
        [InlineData("2n", "123", false)]
        [InlineData("8n6a", "AB", false)]
        [InlineData("1!a", "A", true)]
        [InlineData("1!a1!n", "A1", true)]
        [InlineData("3!c", "d1F", true)]
        [InlineData("2!n", "@#", false)]
        [InlineData("2!n3!a2!c", "12ABCe1", true)]
        [InlineData("2n3a2c", "12ABCe1", true)]
        [InlineData("2n3!a2c", "12123e1", false)]
        [InlineData("2n3a2c", "12123e1", false)]
        [InlineData("2n3a3!c", "", false)]
        [InlineData("2n3a", "12ABCD", false)]
        public void Given_valid_pattern_without_countryCode_it_should_decompose_into_tests(string pattern, string value, bool expectedResult)
        {
            // Act
            IStructureValidator validator = new StructureValidator(_sut.Tokenize(pattern));
            bool result = validator.Validate(value);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("XY2!n", "XY12", true)]
        [InlineData("XY3!n", "XY1234", false)]
        [InlineData("XY2!n", "XY1A", false)]
        [InlineData("XY", "XY", true)]
        [InlineData("XY2n", "XY", false)]
        [InlineData("XY2n", "XY1", true)]
        [InlineData("XY2n", "XY12", true)]
        [InlineData("XY2n", "XY123", false)]
        [InlineData("XY8n6a", "XYAB", false)]
        [InlineData("CD1!a", "CDA", true)]
        [InlineData("AB1!a1!n", "ABA1", true)]
        [InlineData("AB3!c", "ABd1F", true)]
        [InlineData("AB2!n", "XY@#", false)]
        [InlineData("EF2!n3!a2!c", "EF12ABCe1", true)]
        [InlineData("EF2n3a2c", "EF12ABCe1", true)]
        [InlineData("EF2n3!a2c", "EF12123e1", false)]
        [InlineData("EF2n3a2c", "EF12123e1", false)]
        [InlineData("EF2n3a3!c", "EF", false)]
        [InlineData("EF2n3a", "EF12ABCD", false)]
        public void Given_valid_pattern_with_countryCode_it_should_decompose_into_tests(string pattern, string value, bool expectedResult)
        {
            // Act
            IStructureValidator validator = new StructureValidator(_sut.Tokenize(pattern));
            bool result = validator.Validate(value);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("NL2!n", "NL12")]
        [InlineData("NL2!n", "nl12")]
        [InlineData("nL2!n", "nL12")]
        [InlineData("nL2!n", "NL12")]
        [InlineData("nl2!n", "nl12")]
        [InlineData("nl2!n", "NL12")]
        public void Given_mixed_case_country_code_it_should_always_match(string pattern, string value)
        {
            // Act
            IStructureValidator validator = new StructureValidator(_sut.Tokenize(pattern));
            bool result = validator.Validate(value);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("A", "A", 0)]
        [InlineData("2z", "2z", 0)]
        [InlineData("2!n2z", "2z", 1)]
        public void Given_invalid_pattern_when_tokenizing_it_should_throw(string pattern, string token, int pos)
        {
            // Act
            Action act = () => _sut.Tokenize(pattern);

            // Assert
            act.Should()
                .ThrowExactly<PatternException>()
                .WithMessage(string.Format(CultureInfo.CurrentCulture, Resources.ArgumentException_The_structure_segment_0_is_invalid, token, pos) + "*")
                .Which.InnerException.Should()
                .BeNull();
        }
    }
}

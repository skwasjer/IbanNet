using System;
using FluentAssertions;
using Xunit;

namespace IbanNet.Validation
{
    public class SwiftStructureValidationFactoryTests
    {
        private readonly SwiftStructureValidationFactory _sut;

        public SwiftStructureValidationFactoryTests()
        {
            _sut = new SwiftStructureValidationFactory();
        }

        [Fact]
        public void Given_null_pattern_when_creating_validator_it_should_throw()
        {
            string pattern = null;

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            Action act = () => _sut.CreateValidator(string.Empty, pattern);

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(pattern));
        }

        [Theory]
        [InlineData("XY2!n", "XY12", true)]
        [InlineData("XY3!n", "XY1234", false)]
        [InlineData("XY2!n", "XY1A", false)]
        [InlineData("XY2n", "XY", true)]
        [InlineData("XY2n", "XY1", true)]
        [InlineData("XY2n", "XY12", true)]
        [InlineData("XY2n", "XY123", false)]
        [InlineData("CD1!a", "CDA", true)]
        [InlineData("AB1!a1!n", "ABA1", true)]
        [InlineData("AB3!c", "ABd1F", true)]
        [InlineData("AB2!n", "XY@#", false)]
        [InlineData("EF2!n3!a2!c", "EF12ABCe1", true)]
        [InlineData("EF2n3a2c", "EF12ABCe1", true)]
        [InlineData("EF2n3!a2c", "EF12123e1", false)]
        [InlineData("EF2n3a2c", "EF12123e1", false)]
        [InlineData("EF2n3a3!c", "EF", false)]
        public void Given_pattern_it_should_decompose_into_tests(string pattern, string value, bool expectedResult)
        {
            // Act
            IStructureValidator validator = _sut.CreateValidator(string.Empty, pattern);
            bool result = validator.Validate(value);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IbanNet.Registry.Patterns;
using Xunit;
using Xunit.Abstractions;

namespace IbanNet.Registry
{
    public class IbanGeneratorTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IbanValidator _validator;
        private readonly IbanGenerator _sut;

        public IbanGeneratorTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _validator = new IbanValidator();
            _sut = new IbanGenerator();
        }

        [Fact]
        public void Given_null_registry_when_creating_instance_it_should_throw()
        {
            IIbanRegistry registry = null;

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            Func<IbanGenerator> act = () => new IbanGenerator(registry);

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(registry));
        }

        [Fact]
        public void Given_null_countryCode_when_generating_it_should_throw()
        {
            string countryCode = null;

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            Func<Iban> act = () => _sut.Generate(countryCode);

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(countryCode));
        }

        [Fact]
        public void Given_that_countryCode_is_unregistered_when_generating_it_should_throw()
        {
            const string countryCode = "ZZZ";

            // Act
            Func<Iban> act = () => _sut.Generate(countryCode);

            // Assert
            act.Should()
                .ThrowExactly<ArgumentException>()
                .WithMessage("*is not registered*")
                .Which.ParamName.Should()
                .Be(nameof(countryCode));
        }

        [Fact]
        public void Given_that_countryCode_has_no_bban_pattern_when_generating_it_should_throw()
        {
            const string countryCode = "ZZ";
            var registry = new IbanRegistry
            {
                Providers =
                {
                    new IbanRegistryListProvider(new[] { new IbanCountry(countryCode) })
                }
            };
            var sut = new IbanGenerator(registry);

            // Act
            Func<Iban> act = () => sut.Generate(countryCode);

            // Assert
            act.Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage("*does not have a BBAN pattern*");
        }

        [Theory]
        [InlineData("nl")]
        [InlineData("GB")]
        [InlineData("fR")]
        public void Given_mixed_case_country_code_when_generating_it_should_return_valid_iban(string countryCode)
        {
            // Act
            Iban actual = _sut.Generate(countryCode);

            // Assert
            actual.Should().NotBeNull();
            _validator.Validate(actual.ToString()).IsValid.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(GetIbanCountries))]
        public void When_generating_iban_it_should_return_valid_iban(IbanCountry country)
        {
            // Act
            Iban actual = _sut.Generate(country.TwoLetterISORegionName);
            _testOutputHelper.WriteLine(actual.ToString());

            // Assert
            actual.Should().NotBeNull();
            string iban = actual.ToString();
            iban.Length.Should().Be(country.Iban.Length);
            _validator.Validate(iban).IsValid.Should().BeTrue();
        }

        [Fact]
        public void When_generating_ibans_it_should_never_generate_same()
        {
            const int count = 1000;

            // Act
            var ibans = new HashSet<Iban>(
                Enumerable
                    .Range(0, count)
                    .Select(_ => _sut.Generate("NL"))
            );

            // Assert
            ibans.Should().HaveCount(count);
        }

        public static IEnumerable<object[]> GetIbanCountries()
        {
            return IbanRegistry.Default.Select(country => new object[] { country });
        }

        public class GeneratorTests
        {
            [Fact]
            public void Given_a_pattern_of_multiple_tokens_when_generating_it_should_return_expected()
            {
                Pattern pattern = new FakePattern(new[] { new PatternToken(AsciiCategory.Digit, 4), new PatternToken(AsciiCategory.Space, 1), new PatternToken(AsciiCategory.LowercaseLetter, 4), new PatternToken(AsciiCategory.Space, 1), new PatternToken(AsciiCategory.UppercaseLetter, 4), new PatternToken(AsciiCategory.Space, 1), new PatternToken(AsciiCategory.Letter, 4), new PatternToken(AsciiCategory.Space, 1), new PatternToken(AsciiCategory.AlphaNumeric, 4), });

                // Act
                string actual = IbanGenerator.Generator.Random(pattern);

                // Assert
                var segments = actual.Split(' ')
                    .Select(x => x.ToCharArray())
                    .ToList();

                segments.Should().HaveCount(5);
                segments[0].Should().OnlyContain(ch => char.IsDigit(ch));
                segments[1].Should().OnlyContain(ch => char.IsLower(ch));
                segments[2].Should().OnlyContain(ch => char.IsUpper(ch));
                segments[3].Should().OnlyContain(ch => char.IsLetter(ch));
                segments[4].Should().OnlyContain(ch => char.IsLetterOrDigit(ch));
            }

            [Theory]
            [MemberData(nameof(GetRandomPerCategoryTests))]
            public void Given_a_category_when_generating_token_it_should_only_contain_chars_from_category(AsciiCategory category, Func<char, bool> assert)
            {
                var token = new PatternToken(category, 100);

                // Act
                string actual = IbanGenerator.Generator.Random(token);

                // Assert
                actual.ToCharArray().Should().OnlyContain(ch => assert(ch));
            }

            [Theory]
            [InlineData(AsciiCategory.Space, 1, 100)]
            [InlineData(AsciiCategory.Digit, 99, 100)]
            [InlineData(AsciiCategory.LowercaseLetter, 40, 100)]
            [InlineData(AsciiCategory.UppercaseLetter, 12, 75)]
            [InlineData(AsciiCategory.Letter, 3, 19)]
            [InlineData(AsciiCategory.AlphaNumeric, 70, 80)]
            public void Given_min_and_max_length_differ_when_generating_random_token_it_should_have_random_length(AsciiCategory category, int minLength, int maxLength)
            {
                var token = new PatternToken(category, minLength, maxLength);

                // Act
                string actual = IbanGenerator.Generator.Random(token);

                // Assert
                actual.ToCharArray()
                    .Should()
                    .HaveCountGreaterOrEqualTo(minLength)
                    .And
                    .HaveCountLessOrEqualTo(maxLength);
            }

            [Theory]
            [InlineData(AsciiCategory.Space, 1)]
            [InlineData(AsciiCategory.Digit, 99)]
            [InlineData(AsciiCategory.LowercaseLetter, 40)]
            [InlineData(AsciiCategory.UppercaseLetter, 12)]
            [InlineData(AsciiCategory.Letter, 3)]
            [InlineData(AsciiCategory.AlphaNumeric, 70)]
            public void Given_length_when_generating_random_token_it_should_have_exact_length(AsciiCategory category, int length)
            {
                var token = new PatternToken(category, length);

                // Act
                string actual = IbanGenerator.Generator.Random(token);

                // Assert
                actual.Should().HaveLength(length);
            }

            public static IEnumerable<object[]> GetRandomPerCategoryTests()
            {
                yield return new object[] { AsciiCategory.Space, (Func<char, bool>)char.IsWhiteSpace };
                yield return new object[] { AsciiCategory.Digit, (Func<char, bool>)char.IsDigit };
                yield return new object[] { AsciiCategory.LowercaseLetter, (Func<char, bool>)char.IsLower };
                yield return new object[] { AsciiCategory.UppercaseLetter, (Func<char, bool>)char.IsUpper };
                yield return new object[] { AsciiCategory.Letter, (Func<char, bool>)char.IsLetter };
                yield return new object[] { AsciiCategory.AlphaNumeric, (Func<char, bool>)char.IsLetterOrDigit };
            }
        }

        private class FakePattern : Pattern
        {
            public FakePattern(IEnumerable<PatternToken> tokens) : base(tokens)
            {
            }
        }
    }
}

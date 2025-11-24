using IbanNet.Registry;
using IbanNet.Registry.Patterns;
using IbanNet.Registry.Swift;
using IbanNet.Validation.Results;
using TestHelpers;

namespace IbanNet;

public class IbanTests
{
    public class When_creating : IbanTests
    {
        [Fact]
        public void With_null_iban_it_should_throw()
        {
            string? iban = null;

            // Act
            Func<Iban> act = () => new Iban(iban!, IbanRegistry.Default.First());

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName(nameof(iban));
        }

        [Fact]
        public void With_null_country_it_should_throw()
        {
            IbanCountry? ibanCountry = null;

            // Act
            Func<Iban> act = () => new Iban(TestValues.ValidIban, ibanCountry!);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName(nameof(ibanCountry));
        }

        [Theory]
        [InlineData(TestValues.ValidIbanPartitioned, TestValues.ValidIban)]
        [InlineData(TestValues.ValidIbanPartitionedAndWithLowercase, TestValues.ValidIban)]
        [InlineData(TestValues.ValidIban, TestValues.ValidIban)]
        public void It_should_normalize(string iban, string expected)
        {
            // Act
            var actual = new Iban(iban, IbanRegistry.Default[iban.Substring(0, 2)]);

            // Assert
            actual.ToString().Should().Be(expected);
        }
    }

    public class When_formatting : IbanTests
    {
        private const string IbanElectronic = "AD1200012030200359100100";
        private const string IbanPrint = "AD12 0001 2030 2003 5910 0100";
        private const string IbanObfuscated = "XXXXXXXXXXXXXXXXXXXX0100";
        private const string IbanHidden = "****";

        private readonly Iban _iban;

        public When_formatting()
        {
            _iban = new IbanParser(IbanRegistry.Default).Parse(IbanElectronic);
        }

        [Fact]
        public void With_default_format_should_return_electronic()
        {
            // Act
            string actual = _iban.ToString();

            // Assert
            actual.Should().Be(IbanElectronic);
        }

        [Fact]
        public void With_invalid_iban_format_should_throw()
        {
            const IbanFormat format = (IbanFormat)int.MaxValue;

            // Act
            Action act = () => _iban.ToString(format);

            // Assert
            act.Should()
                .Throw<ArgumentException>("the provided format was invalid")
                .WithParameterName(nameof(format));
        }

        [Theory]
        [InlineData(IbanFormat.Electronic, IbanElectronic)]
        [InlineData(IbanFormat.Print, IbanPrint)]
        [InlineData(IbanFormat.Obfuscated, IbanObfuscated)]
        [InlineData(IbanFormat.Hidden, IbanHidden)]
        public void With_valid_iban_format_should_succeed(IbanFormat format, string expected)
        {
            // Act
            string actual = _iban.ToString(format);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void With_invalid_iban_string_format_should_throw()
        {
            const string format = "bad";

            // Act
            Action act = () => _iban.ToString(format);

            // Assert
            act.Should().Throw<IbanFormatException>("the provided format was invalid");
        }

        [Theory]
        [InlineData("E", IbanElectronic)]
        [InlineData("P", IbanPrint)]
        [InlineData("O", IbanObfuscated)]
        [InlineData("H", IbanHidden)]
        public void With_valid_iban_format_when_string_formatting_it_should_succeed(string format, string expected)
        {
            // Act
            string actual = string.Format($"{{0:{format}}}", _iban);

            // Assert
            actual.Should().Be(expected);
        }
    }

    public class When_comparing_for_equality : IbanTests
    {
        private readonly Iban _iban;
        private readonly Iban _equalIban;
        private readonly Iban _otherIban;

        public When_comparing_for_equality()
        {
            var ibanParser = new IbanParser(IbanValidatorStub.Create());

            _iban = ibanParser.Parse(TestValues.ValidIban);
            _equalIban = ibanParser.Parse(TestValues.ValidIbanPartitioned);
            _otherIban = ibanParser.Parse("AE070331234567890123456");
        }

        [Fact]
        public void When_values_are_equal_should_return_true()
        {
            // Act
            bool actual = _iban == _equalIban;

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void By_reference_when_values_are_equal_should_return_true()
        {
            // Act
            bool actual = _iban.Equals(_equalIban);

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void By_reference_when_other_is_null_should_return_false()
        {
            Iban? nullIban = null;

            // Act
            bool actual = _iban.Equals(nullIban);

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void By_reference_when_other_is_self_should_return_true()
        {
            // Act
            // ReSharper disable once EqualExpressionComparison
            bool actual = _iban.Equals(_iban);

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void By_reference_when_other_is_wrong_type_should_return_false()
        {
            object otherType = new();

            // Act
            bool actual = _iban.Equals(otherType);

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void When_values_are_not_equal_should_return_false()
        {
            // Act
            bool actual = _iban == _otherIban;

            // Assert
            actual.Should().BeFalse();
        }
    }

    public class When_comparing_for_inequality : IbanTests
    {
        private readonly Iban _iban;
        private readonly Iban _equalIban;
        private readonly Iban _otherIban;

        public When_comparing_for_inequality()
        {
            var ibanParser = new IbanParser(IbanValidatorStub.Create());

            _iban = ibanParser.Parse(TestValues.ValidIban);
            _equalIban = ibanParser.Parse(TestValues.ValidIbanPartitioned);
            _otherIban = ibanParser.Parse("AE070331234567890123456");
        }

        [Fact]
        public void When_values_are_not_equal_should_return_true()
        {
            // Act
            bool actual = _iban != _otherIban;

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void By_reference_when_values_are_not_equal_should_return_false()
        {
            // Act
            bool actual = _iban.Equals(_otherIban);

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void When_values_are_equal_should_return_false()
        {
            // Act
            bool actual = _iban != _equalIban;

            // Assert
            actual.Should().BeFalse();
        }
    }

    public class When_getting_hashcode : IbanTests
    {
        [Fact]
        public void It_should_be_same_as_underlying_string_value()
        {
            Iban iban = new IbanParser(IbanRegistry.Default).Parse(TestValues.ValidIban);
            int expectedHashCode = TestValues.ValidIban.GetHashCode();

            // Act
            int actual = iban.GetHashCode();

            // Assert
            actual.Should().Be(expectedHashCode);
        }
    }

    public class When_getting_properties : IbanTests
    {
        [Fact]
        public void Given_that_patternDescriptor_exists_it_should_return_extracted_properties()
        {
            IbanCountry ibanCountry = IbanRegistry.Default["AD"];
            Iban iban = new IbanParser(IbanRegistry.Default).Parse(ibanCountry.Iban.Example);
            iban.Country.Should().BeSameAs(ibanCountry);

            // Act & Assert
            iban.Bban.Should().Be(ibanCountry.Bban.Example);
            iban.BankIdentifier.Should().Be(ibanCountry.Bank.Example);
            iban.BranchIdentifier.Should().Be(ibanCountry.Branch.Example);
        }

        [Fact]
        public void Given_that_patternDescriptor_is_not_known_it_should_return_null()
        {
            var ibanCountry = new IbanCountry("NL")
            {
                Iban = new PatternDescriptor(new TestPattern("NL2!n4!a10!n", new SwiftPatternTokenizer()))
                {
                    Example = "NL91ABNA0417164300"
                },
                Bban = new PatternDescriptor(new TestPattern("4!a10!n", new SwiftPatternTokenizer()), 4)
                {
                    Example = "ABNA0417164300"
                }
            };

            var iban = new Iban(ibanCountry.Iban.Example, ibanCountry);
            iban.Country.Should().BeSameAs(ibanCountry);

            // Act & Assert
            iban.Bban.Should().Be(ibanCountry.Bban.Example);
            iban.BankIdentifier.Should().BeNull("no pattern descriptor is available");
            iban.BranchIdentifier.Should().BeNull("no pattern descriptor is available");
        }

        [Fact]
        public void Given_that_bban_patternDescriptor_is_not_known_it_should_not_throw_and_return_iban_substr()
        {
            var ibanCountry = new IbanCountry("NL")
            {
                Iban = new PatternDescriptor(new TestPattern("NL2!n4!a10!n", new SwiftPatternTokenizer()))
                {
                    Example = "NL91ABNA0417164300"
                }
            };

            var iban = new Iban(ibanCountry.Iban.Example, ibanCountry);

            // Act
            Func<string> act = () => iban.Bban;

            // Assert
            act.Should().NotThrow().Which.Should().Be("ABNA0417164300");
        }
    }

#if NET8_0_OR_GREATER
    public class When_system_text_json_converting : IbanTests
    {
        [Theory]
        [InlineData(TestValues.ValidIban, "\"" + TestValues.ValidIban + "\"")]
        [InlineData(null, "null")]
        public void Given_an_iban_when_serializing_it_should_return_expected_json(string? ibanStr, string expectedJson)
        {
            Iban? iban = ibanStr is null ? null : new IbanParser(IbanRegistry.Default).Parse(ibanStr);

            // Act
            string json = System.Text.Json.JsonSerializer.Serialize(iban);

            // Assert
            json.Should().Be(expectedJson);
        }

        [Theory]
        [InlineData(TestValues.ValidIban, "\"" + TestValues.ValidIban + "\"")]
        [InlineData(null, "null")]  // JSON null.
        [InlineData(null, "\"\"")]  // Empty string
        [InlineData(null, "\" \"")] // String with whitespace
        public void Given_a_valid_jsonString_when_deserializing_it_should_return_expected_iban(string? expectedIban, string json)
        {
            // Act
            Iban? iban = System.Text.Json.JsonSerializer.Deserialize<Iban>(json);

            // Assert
            iban?.ToString().Should().Be(expectedIban);
        }

        [Fact]
        public void Given_that_json_iban_is_invalid_when_deserializing_it_should_throw()
        {
            const string jsonStrWithInvalidIban = "\"123\"";

            // Act
            Action act = () => System.Text.Json.JsonSerializer.Deserialize<Iban>(jsonStrWithInvalidIban);

            // Assert
            act.Should()
                .Throw<System.Text.Json.JsonException>()
                .WithMessage("The JSON value could not be converted to IbanNet.Iban*");
        }
    }
#endif

    public sealed class When_parsing_iban : IbanTests
    {
        [Fact]
        public void With_null_value_should_throw()
        {
            string? value = null;

            // Act
            Action act = () => Iban.Parse(value!);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>("the provided value was null")
                .WithParameterName(nameof(value));
        }

        [Fact]
        public void With_invalid_value_should_throw()
        {
            // Act
            Action act = () => Iban.Parse(TestValues.InvalidIban);

            // Assert
            IbanFormatException ex = act.Should().Throw<IbanFormatException>("the provided value was invalid").Which;
            ex.Result.Should().BeEquivalentTo(new ValidationResult
            {
                Error = new IllegalCountryCodeCharactersResult(0),
                AttemptedValue = TestValues.InvalidIban
            });
            ex.InnerException.Should().BeNull();
        }

        [Theory]
        [InlineData("NL91 ABNA 0417 1643 00")]
        [InlineData("NL91\tABNA\t0417\t1643\t00")]
        [InlineData(" NL91 ABNA041 716 4300 ")]
        [InlineData("nl91 ABNA041716\t4300")]
        public void Given_that_iban_contains_whitespace_or_lowercase_when_parsing_it_should_succeed(string iban)
        {
            const string expectedNormalizedIban = "NL91ABNA0417164300";

            // Act
            var actual = Iban.Parse(iban);

            // Assert
            actual.ToString().Should().Be(expectedNormalizedIban);
        }

        [Fact]
        public void With_valid_value_should_return_iban()
        {
            Iban? iban = null;

            // Act
            Action act = () => iban = Iban.Parse(TestValues.ValidIban);

            // Assert
            act.Should().NotThrow<IbanFormatException>();
            iban.Should()
                .NotBeNull("the value should be parsed")
                .And.BeOfType<Iban>()
                .Which.ToString()
                .Should()
                .Be(TestValues.ValidIban, "the returned value should match the provided value");
        }
    }

    public sealed class When_trying_to_parse_iban : IbanTests
    {
        [Fact]
        public void With_null_value_should_return_false()
        {
            // Act
            bool actual = Iban.TryParse(null, out Iban? iban);

            // Assert
            actual.Should().BeFalse("the provided value was null which is not valid");
            iban.Should().BeNull("parsing did not succeed");
        }

        [Fact]
        public void With_invalid_value_should_return_false()
        {
            // Act
            bool actual = Iban.TryParse(TestValues.InvalidIban, out Iban? iban);

            // Assert
            actual.Should().BeFalse("the provided value was invalid");
            iban.Should().BeNull("parsing did not succeed");
        }

        [Fact]
        public void With_valid_value_should_pass()
        {
            // Act
            bool actual = Iban.TryParse(TestValues.ValidIban, out Iban? iban);

            // Assert
            actual.Should().BeTrue("the provided value was valid");
            iban.Should()
                .NotBeNull()
                .And.BeOfType<Iban>()
                .Which.ToString()
                .Should()
                .Be(TestValues.ValidIban);
        }
    }

}

using System;
using System.Linq;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Registry.Swift;
using TestHelpers;
using Xunit;

namespace IbanNet
{
    public class IbanTests
    {
        public class When_creating : IbanTests
        {
            [Fact]
            public void With_null_iban_it_should_throw()
            {
                string iban = null;

                // Act
                // ReSharper disable once AssignNullToNotNullAttribute
                Func<Iban> act = () => new Iban(iban, IbanRegistry.Default.First());

                // Assert
                act.Should()
                    .ThrowExactly<ArgumentNullException>()
                    .Which.ParamName.Should()
                    .Be(nameof(iban));
            }

            [Fact]
            public void With_null_country_it_should_throw()
            {
                IbanCountry ibanCountry = null;

                // Act
                // ReSharper disable once AssignNullToNotNullAttribute
                Func<Iban> act = () => new Iban(TestValues.ValidIban, ibanCountry);

                // Assert
                act.Should()
                    .ThrowExactly<ArgumentNullException>()
                    .Which.ParamName.Should()
                    .Be(nameof(ibanCountry));
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
                    .Which.ParamName.Should()
                    .Be(nameof(format));
            }

            [Theory]
            [InlineData(IbanFormat.Electronic, IbanElectronic)]
            [InlineData(IbanFormat.Print, IbanPrint)]
            [InlineData(IbanFormat.Obfuscated, IbanObfuscated)]
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
                var ibanParser = new IbanParser(new IbanValidatorStub());

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
                Iban nullIban = null;

                // Act
                // ReSharper disable once ExpressionIsAlwaysNull
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
                var ibanParser = new IbanParser(new IbanValidatorStub());

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

        public class When_normalizing
        {
            [Theory]
            [InlineData("no-whitespace", "NO-WHITESPACE")]
            [InlineData(" \tin-\nstr ing\r", "IN-STRING")]
            [InlineData("(&*!S #%t", "(&*!S#%T")]
            [InlineData("", "")]
            [InlineData(null, null)]
            public void Given_string_when_normalizing_it_should_return_expected_value(string input, string expected)
            {
                // Act
                string actual = Iban.NormalizeOrNull(input);

                // Assert
                actual.Should().Be(expected);
            }

#if USE_SPANS
            [Fact]
            public void Given_that_string_exceeds_max_stackalloc_length_when_normalizing_it_should_return_expected_value()
            {
                string spaces = new(' ', 50);
                string input = spaces + " \tin-\nstr ing\r" + spaces;
                input.Length.Should().BeGreaterThan(Iban.MaxLength * 2);
                const string expected = "IN-STRING";

                // Act
                string actual = Iban.NormalizeOrNull(input);

                // Assert
                actual.Should().Be(expected);
            }
#endif
        }

        public class When_getting_properties : IbanTests
        {
            [Fact]
            public void Given_that_structure_sections_are_known_it_should_return_extracted_properties()
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
            public void Given_that_structure_sections_are_not_known_it_should_return_null()
            {
                var ibanCountry = new IbanCountry("NL")
                {
                    Iban = new IbanStructure(new IbanSwiftPattern("NL2!n4!a10!n"))
                    {
                        Example = "NL91ABNA0417164300"
                    },
                    Bban = new BbanStructure(new SwiftPattern("4!a10!n"), 4)
                    {
                        Example = "ABNA0417164300"
                    }
                };

                var iban = new Iban(ibanCountry.Iban.Example, ibanCountry);
                iban.Country.Should().BeSameAs(ibanCountry);

                // Act & Assert
                iban.Bban.Should().Be(ibanCountry.Bban.Example);
                iban.BankIdentifier.Should().BeNull();
                iban.BranchIdentifier.Should().BeNull();
            }

            [Fact]
            public void Given_that_bban_structure_section_is_not_known_it_should_not_throw_and_return_iban_substr()
            {
                var ibanCountry = new IbanCountry("NL")
                {
                    Iban = new IbanStructure(new IbanSwiftPattern("NL2!n4!a10!n"))
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

#if NET5_0_OR_GREATER
        public class When_system_text_json_converting : IbanTests
        {
            [Theory]
            [InlineData(TestValues.ValidIban, "\""+ TestValues.ValidIban + "\"")]
            [InlineData(null, "null")]
            public void Given_an_iban_when_serializing_it_should_return_expected_json(string ibanStr, string expectedJson)
            {
                Iban iban = ibanStr is null ? null : new IbanParser(IbanRegistry.Default).Parse(ibanStr);

                // Act
                string json = System.Text.Json.JsonSerializer.Serialize(iban);

                // Assert
                json.Should().Be(expectedJson);
            }

            [Theory]
            [InlineData(TestValues.ValidIban, "\""+ TestValues.ValidIban + "\"")]
            [InlineData(null, "null")]  // JSON null.
            [InlineData(null, "\"\"")]  // Empty string
            [InlineData(null, "\" \"")] // String with whitespace
            public void Given_a_valid_jsonString_when_deserializing_it_should_return_expected_iban(string expectedIban, string json)
            {
                // Act
                Iban iban = System.Text.Json.JsonSerializer.Deserialize<Iban>(json);

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
                    .ThrowExactly<System.Text.Json.JsonException>()
                    .WithMessage("The JSON value could not be converted to IbanNet.Iban*");
            }
        }
#endif
    }
}

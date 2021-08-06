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
            private readonly Iban _iban;

            public When_formatting()
            {
                _iban = new IbanParser(IbanRegistry.Default).Parse(TestValues.ValidIban);
            }

            [Fact]
            public void With_default_format_should_return_flat()
            {
                // Act
                string actual = _iban.ToString();

                // Assert
                actual.Should().Be(TestValues.ValidIban);
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
            [InlineData(IbanFormat.Electronic, TestValues.ValidIban)]
            [InlineData(IbanFormat.Print, TestValues.ValidIbanPartitioned)]
            [InlineData(IbanFormat.Obfuscated, "XXXXXXXXXXXXXXXXXXXX0100")]
            public void With_valid_iban_format_should_succeed(IbanFormat format, string expected)
            {
                // Act
                string actual = _iban.ToString(format);

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
                // ReSharper disable once AssignNullToNotNullAttribute
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
                var otherType = new object();

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

        public class When_checking_qr_iban : IbanTests
        {
            [Theory]
            [InlineData("CH58 3000 0000 0000 0000 0")]
            [InlineData("CH07 3100 0000 0000 0000 0")]
            [InlineData("CH54 3100 0111 1111 1111 1")]
            [InlineData("CH45 3199 9000 0000 0000 0")]
            [InlineData("LI71 3000 0000 0000 0000 0")]
            [InlineData("LI20 3100 0000 0000 0000 0")]
            [InlineData("LI67 3100 0111 1111 1111 1")]
            [InlineData("LI58 3199 9000 0000 0000 0")]
            public void Given_iban_is_a_valid_qr_iban_when_getting_IsQrIban_it_should_be_true(string ibanString)
            {
                Iban iban = new IbanParser(IbanRegistry.Default).Parse(ibanString);

                iban.IsQrIban.Should().BeTrue();
            }

            [Theory]
            [InlineData("CH50 2999 9000 0000 0000 0")]
            [InlineData("LI63 2999 9000 0000 0000 0")]
            [InlineData("CH53 3200 0000 0000 0000 0")]
            [InlineData("LI66 3200 0000 0000 0000 0")]
            [InlineData("DE61 0003 0000 1111 1111 11")]
            public void Given_iban_is_valid_but_not_a_valid_qr_iban_when_getting_IsQrIban_it_should_be_false(string ibanString)
            {
                Iban iban = new IbanParser(IbanRegistry.Default).Parse(ibanString);

                iban.IsQrIban.Should().BeFalse();
            }
        }
    }
}

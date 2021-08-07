using System;
using System.Linq;
using FluentAssertions;
using IbanNet.Registry;
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
            private readonly Iban _iban;
            private readonly IbanCountry _ibanCountry;

            public When_getting_properties()
            {
                _iban = new IbanParser(IbanRegistry.Default).Parse(TestValues.ValidIban);
                _ibanCountry = IbanRegistry.Default[TestValues.ValidIban.Substring(0, 2)];
            }

            [Fact]
            public void It_should_return_country()
            {
                _iban.Country.Should().BeSameAs(_ibanCountry);
            }
        }
    }
}

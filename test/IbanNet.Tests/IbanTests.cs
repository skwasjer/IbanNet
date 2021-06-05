using System;
using FluentAssertions;
using Xunit;

namespace IbanNet
{
    public class IbanTests
    {
        public class When_creating : IbanTests
        {
            [Fact]
            public void With_null_it_should_throw()
            {
                string iban = null;

                // Act
                // ReSharper disable once ObjectCreationAsStatement
                // ReSharper disable once AssignNullToNotNullAttribute
                Func<Iban> act = () => new Iban(iban);

                // Assert
                act.Should()
                    .ThrowExactly<ArgumentNullException>()
                    .Which.ParamName.Should()
                    .Be(nameof(iban));
            }

            [Theory]
            [InlineData(TestValues.ValidIbanPartitioned, TestValues.ValidIban)]
            [InlineData(TestValues.ValidIbanPartitionedAndWithLowercase, TestValues.ValidIban)]
            [InlineData(TestValues.ValidIban, TestValues.ValidIban)]
            public void It_should_normalize(string iban, string expected)
            {
                // Act
                var actual = new Iban(iban);

                // Assert
                actual.ToString().Should().Be(expected);
            }
        }

        public class When_formatting : IbanTests
        {
            private readonly Iban _iban;

            public When_formatting()
            {
                _iban = new Iban(TestValues.ValidIban);
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
                var ibanParser = new IbanParser(new IbanValidatorMock());

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
                var ibanParser = new IbanParser(new IbanValidatorMock());

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
                var iban = new Iban(TestValues.ValidIban);
                int expectedHashCode = TestValues.ValidIban.GetHashCode();

                // Act
                int actual = iban.GetHashCode();

                // Assert
                actual.Should().Be(expectedHashCode);
            }
        }
    }
}

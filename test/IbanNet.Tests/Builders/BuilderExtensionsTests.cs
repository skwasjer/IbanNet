using System;
using System.Collections.Generic;
using FluentAssertions;
using IbanNet.Registry;
using Moq;
using Xunit;

namespace IbanNet.Builders
{
    public class BuilderExtensionsTests
    {
        public class GetIbanBuilderTests : BuilderExtensionsTests
        {
            [Fact]
            public void Given_country_when_getting_iban_builder_it_should_return_instance()
            {
                var country = new IbanCountry("XX");

                // Act
                IbanBuilder actual = country.GetIbanBuilder();

                // Assert
                actual.Should().NotBeNull();
            }

            [Fact]
            public void Given_null_country_when_getting_iban_builder_it_should_throw()
            {
                IbanCountry country = null;

                // Act
                // ReSharper disable once AssignNullToNotNullAttribute
                Action act = () => country.GetIbanBuilder();

                // Assert
                act.Should()
                    .ThrowExactly<ArgumentNullException>()
                    .Which.ParamName.Should()
                    .Be(nameof(country));
            }
        }

        public class GetBbanBuilderTests : BuilderExtensionsTests
        {
            [Fact]
            public void Given_country_when_getting_bban_builder_it_should_return_instance()
            {
                var country = new IbanCountry("XX");

                // Act
                BbanBuilder actual = country.GetBbanBuilder();

                // Assert
                actual.Should().NotBeNull();
            }

            [Fact]
            public void Given_null_country_when_getting_bban_builder_it_should_throw()
            {
                IbanCountry country = null;

                // Act
                // ReSharper disable once AssignNullToNotNullAttribute
                Action act = () => country.GetBbanBuilder();

                // Assert
                act.Should()
                    .ThrowExactly<ArgumentNullException>()
                    .Which.ParamName.Should()
                    .Be(nameof(country));
            }
        }

        public class WithCountryTests : BuilderExtensionsTests
        {
            [Theory]
            [MemberData(nameof(WithCountryTestCases))]
            public void Given_invalid_arg_when_getting_builder_with_countryCode_it_should_throw(IBankAccountBuilder builder, string countryCode, IIbanRegistry registry, string expectedParamName)
            {
                // Act
                Action act = () => builder.WithCountry(countryCode, registry);

                // Assert
                act.Should()
                    .Throw<ArgumentException>()
                    .Which.ParamName.Should()
                    .Be(expectedParamName);
            }

            [Theory]
            [InlineData("NL")]
            [InlineData("GB")]
            public void When_getting_builder_with_countryCode_it_should_configure_builder(string countryCode)
            {
                IBankAccountBuilder builder = new IbanBuilder();

                // Act
                builder.WithCountry(countryCode, IbanRegistry.Default);

                // Assert
                string iban = builder.Build();
                iban.Should().StartWith(countryCode);
            }

            public static IEnumerable<object[]> WithCountryTestCases()
            {
                IBankAccountBuilder builder = Mock.Of<IBankAccountBuilder>();
                const string countryCode = "NL";
                IIbanRegistry registry = Mock.Of<IIbanRegistry>();

                IbanCountry other = null;
                var nl = new IbanCountry(countryCode);
                Mock.Get(registry).Setup(m => m.TryGetValue(It.IsAny<string>(), out other));
                Mock.Get(registry).Setup(m => m.TryGetValue("NL", out nl));

                yield return new object[] { null, countryCode, registry, nameof(builder) };
                yield return new object[] { builder, null, registry, nameof(countryCode) };
                yield return new object[] { builder, countryCode, null, nameof(registry) };
                yield return new object[] { builder, "ZZ", registry, nameof(countryCode) };
            }
        }
    }
}

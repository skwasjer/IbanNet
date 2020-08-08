using System.Linq;
using FluentAssertions;
using IbanNet;
using IbanNet.DependencyInjection;
using IbanNet.Registry;
using TestHelpers.Fixtures;
using Xunit;

namespace TestHelpers.Specs
{
    public abstract class ConfiguredRegistrySpec : DiSpec
    {
        protected ConfiguredRegistrySpec(IDependencyInjectionFixture fixture)
            : base(fixture)
        {
        }

        protected override void Given()
        {
            Fixture.Configure(builder =>
            {
                IIbanNetOptionsBuilder returnedBuilder = builder.UseRegistry(
                    new SwiftRegistryProvider().Where(c => c.TwoLetterISORegionName == "NL" || c.TwoLetterISORegionName == "GB")
                );

                returnedBuilder.Should().BeSameAs(builder);
            });
        }

        [Theory]
        [InlineData("NL91ABNA0417164300")]
        [InlineData("GB29NWBK60161331926819")]
        public void When_validating_with_registered_country_it_should_pass(string validIban)
        {
            IIbanValidator validator = Subject.GetRequiredService<IIbanValidator>();
            validator.Validate(validIban).IsValid.Should().BeTrue();
        }

        [Fact]
        public void When_validating_with_unregistered_country_it_should_fail()
        {
            IIbanValidator validator = Subject.GetRequiredService<IIbanValidator>();
            validator.Validate("DE89370400440532013000").IsValid.Should().BeFalse();
        }
    }
}

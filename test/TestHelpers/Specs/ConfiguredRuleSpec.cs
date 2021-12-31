using IbanNet;
using IbanNet.DependencyInjection;
using TestHelpers.FakeRules;
using TestHelpers.Fixtures;
using TestHelpers.FluentAssertions;

namespace TestHelpers.Specs
{
    public abstract class ConfiguredRuleSpec : DiSpec
    {
        protected ConfiguredRuleSpec(IDependencyInjectionFixture fixture) : base(fixture)
        {
        }

        protected override void Given()
        {
            Fixture.Configure(builder =>
            {
                IIbanNetOptionsBuilder returnedBuilder = builder.WithRule(typeof(TestValidationRule));

                returnedBuilder.Should().BeSameAs(builder);
            });
        }

        [Fact]
        public void Given_rule_is_registered_when_resolving_options_it_should_have_rule()
        {
            // Assert
            IIbanValidator validator = Subject.GetService<IIbanValidator>();
            validator.Should()
                .BeOfType<IbanValidator>()
                .Which.Options.Should()
                .HaveRule<TestValidationRule>()
                .And.HaveCount(1);
        }
    }
}

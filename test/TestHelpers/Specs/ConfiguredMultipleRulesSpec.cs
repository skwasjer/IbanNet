using IbanNet;
using IbanNet.DependencyInjection;
using IbanNet.Validation.Rules;
using TestHelpers.FakeRules;
using TestHelpers.Fixtures;
using TestHelpers.FluentAssertions;

namespace TestHelpers.Specs;

public abstract class ConfiguredMultipleRulesSpec : DiSpec
{
    private Type _fakeRuleType = default!;

    protected ConfiguredMultipleRulesSpec(IDependencyInjectionFixture fixture) : base(fixture)
    {
    }

    protected override void Given()
    {
        IIbanValidationRule fakeRule = Mock.Of<IIbanValidationRule>();
        _fakeRuleType = fakeRule.GetType();
        Fixture.Configure(builder =>
        {
            IIbanNetOptionsBuilder returnedBuilder = builder
                .WithRule(typeof(TestValidationRule))
                .WithRule<AnotherTestValidationRule>()
                .WithRule(() => fakeRule);

            returnedBuilder.Should().BeSameAs(builder);
        });
    }

    [Fact]
    public void Given_two_rules_are_registered_when_resolving_options_it_should_have_rules_in_order()
    {
        // Assert
        IIbanValidator? validator = Subject.GetService<IIbanValidator>();
        var rules = validator.Should()
            .BeOfType<IbanValidator>()
            .Which.Options.Should()
            .HaveRule<IIbanValidationRule>()
            .And.HaveCount(3)
            .And.Subject.ToList();

        rules[0].Should().BeOfType<TestValidationRule>();
        rules[1].Should().BeOfType<AnotherTestValidationRule>();
        rules[2].Should().BeOfType(_fakeRuleType);
    }
}

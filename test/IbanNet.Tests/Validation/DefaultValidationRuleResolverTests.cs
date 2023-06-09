using IbanNet.Registry;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation;

public class DefaultValidationRuleResolverTests
{
    private readonly DefaultValidationRuleResolver _sut;
    private readonly List<IIbanValidationRule> _customRules;

    public DefaultValidationRuleResolverTests()
    {
        var registryMock = new Mock<IIbanRegistry>();
        registryMock.Setup(m => m.Providers).Returns(new List<IIbanRegistryProvider>());
        _customRules = new List<IIbanValidationRule>();
        _sut = new DefaultValidationRuleResolver(registryMock.Object, _customRules);
    }

    [Fact]
    public void Given_strict_method_when_getting_rules_it_should_return_expected_rules()
    {
        // Act
        IEnumerable<IIbanValidationRule> rules = _sut.GetRules();

        // Assert
        rules.Select(r => r.GetType())
            .Should()
            .BeEquivalentTo(
                new[]
                {
                    typeof(NotEmptyRule),
                    typeof(HasCountryCodeRule),
                    typeof(NoIllegalCharactersRule),
                    typeof(HasIbanChecksumRule),
                    typeof(IsValidCountryCodeRule),
                    typeof(IsValidLengthRule),
                    typeof(IsMatchingStructureRule),
                    typeof(Mod97Rule)
                },
                options => options.WithStrictOrdering()
            );
    }

    [Fact]
    public void Given_custom_rules_when_getting_rules_it_should_append_custom_rules()
    {
        IIbanValidationRule rule1 = Mock.Of<IIbanValidationRule>();
        IIbanValidationRule rule2 = Mock.Of<IIbanValidationRule>();
        _customRules.Add(rule1);
        _customRules.Add(rule2);

        // Act
        IEnumerable<IIbanValidationRule> rules = _sut.GetRules();

        // Assert
        rules.Should()
            .EndWith(new[] { rule1, rule2 });
    }
}

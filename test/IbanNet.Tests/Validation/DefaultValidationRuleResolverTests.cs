using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Rules;
using Moq;
using Xunit;

namespace IbanNet.Validation
{
    public class DefaultValidationRuleResolverTests
    {
        private readonly DefaultValidationRuleResolver _sut;
        private readonly IbanValidatorOptions _options;

        public DefaultValidationRuleResolverTests()
        {
            var registryMock = new Mock<IIbanRegistry>();
            registryMock.Setup(m => m.Providers).Returns(new List<IIbanRegistryProvider>());
            _options = new IbanValidatorOptions
            {
                Registry = registryMock.Object
            };
            _sut = new DefaultValidationRuleResolver(_options);
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
            _options.Rules.Add(rule1);
            _options.Rules.Add(rule2);

            // Act
            IEnumerable<IIbanValidationRule> rules = _sut.GetRules();

            // Assert
            rules.Should()
                .EndWith(new[] { rule1, rule2 });
        }
    }
}

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
		private readonly List<IIbanValidationRule> _customRules;
		private readonly IbanValidatorOptions _options;

		public DefaultValidationRuleResolverTests()
		{
			_customRules = new List<IIbanValidationRule>();
			var registryMock = new Mock<IIbanRegistry>();
			registryMock.Setup(m => m.Providers).Returns(new List<IIbanRegistryProvider>());
			_options = new IbanValidatorOptions
			{
				Registry = registryMock.Object,
				Rules = _customRules
			};
			_sut = new DefaultValidationRuleResolver(_options);
		}

		[Fact]
		public void Given_loose_method_when_getting_rules_it_should_return_expected_rules()
		{
			_options.Method = ValidationMethod.Loose;

			// Act
			IEnumerable<IIbanValidationRule> rules = _sut.GetRules();

			// Assert
			rules.Select(r => r.GetType())
				.Should()
				.BeEquivalentTo(
					new[] {
						typeof(NotEmptyRule),
						typeof(HasCountryCodeRule),
						typeof(NoIllegalCharactersRule),
						typeof(HasIbanChecksumRule),
						typeof(IsValidCountryCodeRule),
						typeof(IsValidLengthRule),
						typeof(Mod97Rule)
					},
					options => options.WithStrictOrdering()
				);
		}

		[Fact]
		public void Given_strict_method_when_getting_rules_it_should_return_expected_rules()
		{
			_options.Method = ValidationMethod.Strict;

			// Act
			IEnumerable<IIbanValidationRule> rules = _sut.GetRules();

			// Assert
			rules.Select(r => r.GetType())
				.Should()
				.BeEquivalentTo(
					new[] {
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

		[Theory]
		[InlineData(ValidationMethod.Loose)]
		[InlineData(ValidationMethod.Strict)]
		public void Given_custom_rules_for_any_method_when_getting_rules_it_should_append_custom_rules(ValidationMethod method)
		{
			_options.Method = method;
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
}

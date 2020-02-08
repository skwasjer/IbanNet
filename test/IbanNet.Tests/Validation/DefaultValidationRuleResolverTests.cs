using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Methods;
using IbanNet.Validation.Rules;
using Moq;
using NUnit.Framework;

namespace IbanNet.Validation
{
	[TestFixture]
	public class DefaultValidationRuleResolverTests
	{
		private DefaultValidationRuleResolver _sut;
		private List<IIbanValidationRule> _customRules;
		private IbanValidatorOptions _options;

		[SetUp]
		public void SetUp()
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

		[Test]
		public void Given_loose_method_when_getting_rules_it_should_return_expected_rules()
		{
			_options.ValidationMethod = new LooseValidation();

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

		[Test]
		public void Given_strict_method_when_getting_rules_it_should_return_expected_rules()
		{
			_options.ValidationMethod = new StrictValidation();

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

		[TestCase(typeof(LooseValidation))]
		[TestCase(typeof(StrictValidation))]
		public void Given_custom_rules_for_any_method_when_getting_rules_it_should_append_custom_rules(Type validationMethodType)
		{
			_options.ValidationMethod = (ValidationMethod)Activator.CreateInstance(validationMethodType);
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

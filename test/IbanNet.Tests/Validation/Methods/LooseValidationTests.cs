using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Rules;
using NUnit.Framework;

namespace IbanNet.Validation.Methods
{
	[TestFixture]
	public class LooseValidationTests
	{
		[Test]
		public void When_getting_rules_it_should_return_all_strict_rules()
		{
			var sut = new LooseValidation();

			// Act
			IEnumerable<IIbanValidationRule> rules = sut.GetRules(
				new ReadOnlyDictionary<string, IbanCountry>(new Dictionary<string, IbanCountry>())
			);

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
	}
}

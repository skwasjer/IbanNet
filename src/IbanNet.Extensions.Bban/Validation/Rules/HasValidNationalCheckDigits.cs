using System.Collections.Generic;
using System.Linq;
using IbanNet.Validation.NationalCheckDigits;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the BBAN portion of an IBAN has valid national check digits.
	/// </summary>
	public class HasValidNationalCheckDigits : IIbanValidationRule
	{
		private readonly IReadOnlyDictionary<string, IEnumerable<NationalCheckDigitsValidator>> _nationalCheckDigitsValidators;

		/// <summary>
		/// Initializes a new instance of the <see cref="HasValidNationalCheckDigits"/> class.
		/// </summary>
		public HasValidNationalCheckDigits()
			: this(
				new List<NationalCheckDigitsValidator>
				{
					new CinNationalCheckDigitsValidator(),
					new CleRibNationalCheckDigitsValidator(),
					new NorwayMod11ValidatorDigitsValidator(),
					new BosniaAndHerzegovinaMod97NationalCheckDigitsValidator()
				}
			)
		{
		}

		// For access by unit tests.
		internal HasValidNationalCheckDigits(IEnumerable<NationalCheckDigitsValidator> nationalCheckDigitsValidators)
		{
			// Group national check digits validators by supported countries and then create dictionary for quick resolving.
			_nationalCheckDigitsValidators = nationalCheckDigitsValidators
				.SelectMany(v => v.SupportedCountryCodes
					.Select(c => new
					{
						validator = v,
						countryCode = c
					})
				)
				.GroupBy(g => g.countryCode)
				.ToDictionary(
					g => g.Key,
					g => (IEnumerable<NationalCheckDigitsValidator>)g.Select(kvp => kvp.validator).ToList()
				);
		}

		/// <inheritdoc />
		public void Validate(ValidationRuleContext context)
		{
			if (!_nationalCheckDigitsValidators.TryGetValue(context.Country.TwoLetterISORegionName, out IEnumerable<NationalCheckDigitsValidator> checkDigitsValidators))
			{
				return;
			}

			string bban = context.Value.Substring(4, context.Country.Bban.Length);
			bool success = checkDigitsValidators.Any(validator => validator.Validate(bban));
			if (!success)
			{
				context.Fail("Invalid national check digits.");
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Validators;

namespace IbanNet.FluentValidation
{
	/// <summary>
	/// A property validator to limit international bank account numbers to specific countries.
	/// </summary>
	public class FluentIbanCountryValidator : PropertyValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FluentIbanCountryValidator"/> class using specified country codes.
		/// </summary>
		/// <param name="countryCodes">The accepted country codes.</param>
		public FluentIbanCountryValidator(IEnumerable<string> countryCodes)
			: base("'{PropertyName}' is not valid because it is not an IBAN from a list of accepted countries.")
		{
			if (countryCodes == null)
			{
				throw new ArgumentNullException(nameof(countryCodes));
			}

			CountryCodes = new HashSet<string>(countryCodes);
		}

		/// <summary>
		/// Gets the accepted country codes.
		/// </summary>
		public ICollection<string> CountryCodes { get; }

		/// <inheritdoc />
		protected override bool IsValid(PropertyValidatorContext context)
		{
			return context.PropertyValue == null || CountryCodes.Contains(((string)context.PropertyValue).GetCountryCode(), StringComparer.OrdinalIgnoreCase);
		}
	}
}
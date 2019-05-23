using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IbanNet.DataAnnotations
{
	/// <summary>
	/// When applied to a <see cref="string" /> property or parameter, validates that the country of a IBAN matches the specific set of allowed countries.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
	public class IbanCountryAttribute : ValidationAttribute
	{
		/// <summary>
		/// Gets or sets the country codes that are accepted.
		/// </summary>
		public ICollection<string> CountryCodes { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanCountryAttribute" /> class with specified <paramref name="countryCodes"/>.
		/// </summary>
		/// <param name="countryCodes">The allowed country codes.</param>
		public IbanCountryAttribute(params string[] countryCodes)
			: base(Resources.IbanCountryAttribute_NotAccepted)
		{
			if (countryCodes == null)
			{
				throw new ArgumentNullException(nameof(countryCodes));
			}

			if (countryCodes.Length == 0)
			{
				throw new ArgumentException("Value cannot be an empty collection.", nameof(countryCodes));
			}

			CountryCodes = new HashSet<string>(countryCodes);
		}

		/// <inheritdoc />
		protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return System.ComponentModel.DataAnnotations.ValidationResult.Success;
			}

			if (!(value is string strValue))
			{
				return base.IsValid(value, validationContext);
			}

			string countryCode = strValue.GetCountryCode();
			if (countryCode != null && CountryCodes.Contains(countryCode, StringComparer.OrdinalIgnoreCase))
			{
				return System.ComponentModel.DataAnnotations.ValidationResult.Success;
			}

			IEnumerable<string> memberNames = null;
			if (validationContext.MemberName != null)
			{
				memberNames = new[] { validationContext.MemberName };
			}

			return new System.ComponentModel.DataAnnotations.ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
		}
	}
}
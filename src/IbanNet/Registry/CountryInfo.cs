using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IbanNet.Registry
{
	/// <summary>
	/// Contains IBAN/BBAN format information about the country.
	/// </summary>
	[DebuggerDisplay("\\{{" + nameof(TwoLetterISORegionName) + ",nq} - {" + nameof(EnglishName) + ",nq}\\}")]
	public class CountryInfo
	{
		internal CountryInfo()
		{
			//
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CountryInfo"/> class using specified 2 letter ISO region name.
		/// </summary>
		/// <param name="name">The 2 letter iso region name.</param>
		public CountryInfo(string name)
		{
			TwoLetterISORegionName = name ?? throw new ArgumentNullException(nameof(name));
		}

		/// <summary>
		/// Gets or sets the country code.
		/// </summary>
		// ReSharper disable once InconsistentNaming
		public string TwoLetterISORegionName { get; internal set; }

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the English name.
		/// </summary>
		public string EnglishName { get; set; }

		/// <summary>
		/// Gets a list of included countries.
		/// </summary>
		public IReadOnlyCollection<string> IncludedCountries { get; set; } = new ReadOnlyCollection<string>(new string[0]);

		/// <summary>
		/// Gets SEPA information.
		/// </summary>
		public SepaInfo Sepa { get; set; }

		/// <summary>
		/// Gets a domestic account number example.
		/// </summary>
		public string DomesticAccountNumberExample { get; set; }

		/// <summary>
		/// Gets the structure of the BBAN.
		/// </summary>
		public BbanStructure Bban { get; set; }

		/// <summary>
		/// Gets the structure of the IBAN.
		/// </summary>
		public IbanStructure Iban { get; set; }

		/// <summary>
		/// Gets when this <see cref="CountryInfo"/> was last updated in the Iban Registry.
		/// </summary>
		public DateTimeOffset LastUpdatedDate { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return TwoLetterISORegionName;
		}
	}
}
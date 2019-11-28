using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace IbanNet.Registry
{
	/// <summary>
	/// Contains IBAN/BBAN format information about the country.
	/// </summary>
	[DebuggerDisplay("\\{{" + nameof(TwoLetterISORegionName) + ",nq} - {" + nameof(EnglishName) + ",nq}\\}")]
	[DebuggerStepThrough]
	public class CountryInfo
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string? _displayName;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private BbanStructure? _bbanStructure;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IbanStructure? _ibanStructure;

		/// <summary>
		/// Initializes a new instance of the <see cref="CountryInfo"/> class using specified 2 letter ISO region name.
		/// </summary>
		/// <param name="twoLetterISORegionName">The 2 letter iso region name.</param>
		// ReSharper disable once InconsistentNaming
		public CountryInfo(string twoLetterISORegionName)
		{
			if (twoLetterISORegionName == null)
			{
				throw new ArgumentNullException(nameof(twoLetterISORegionName));
			}

			if (twoLetterISORegionName.Length != 2)
			{
				// TODO: breaking change, wrong exception, should just be ArgumentException.
				throw new ArgumentOutOfRangeException(nameof(twoLetterISORegionName), Resources.ArgumentException_Invalid_country_code);
			}

			TwoLetterISORegionName = twoLetterISORegionName.ToUpperInvariant();
			EnglishName = TwoLetterISORegionName;
		}

		/// <summary>
		/// Gets or sets the country code.
		/// </summary>
		// ReSharper disable once InconsistentNaming
		public string TwoLetterISORegionName { get; }

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		public string DisplayName
		{
			get => _displayName ?? EnglishName;
			set => _displayName = value;
		}

		/// <summary>
		/// Gets or sets the English name.
		/// </summary>
		public string EnglishName { get; set; }

		/// <summary>
		/// Gets or sets the list of included countries.
		/// </summary>
		public IReadOnlyCollection<string> IncludedCountries { get; set; } = new ReadOnlyCollection<string>(new string[0]);

		/// <summary>
		/// Gets SEPA information.
		/// </summary>
		public SepaInfo? Sepa { get; set; }

		/// <summary>
		/// Gets or sets a domestic account number example.
		/// </summary>
		public string? DomesticAccountNumberExample { get; set; }

		/// <summary>
		/// Gets or sets the structure of the BBAN.
		/// </summary>
		[AllowNull]
		public BbanStructure Bban
		{
			get => _bbanStructure ??= new BbanStructure();
			set => _bbanStructure = value;
		}

		/// <summary>
		/// Gets or sets the structure of the IBAN.
		/// </summary>
		[AllowNull]
		public IbanStructure Iban
		{
			get => _ibanStructure ??= new IbanStructure();
			set => _ibanStructure = value;
		}

		/// <summary>
		/// Gets or sets when this <see cref="CountryInfo"/> was last updated in the Iban Registry.
		/// </summary>
		public DateTimeOffset LastUpdatedDate { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return TwoLetterISORegionName;
		}
	}
}

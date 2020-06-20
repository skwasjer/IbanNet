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
    public class IbanCountry
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string? _displayName;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IbanStructure? _ibanStructure;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BbanStructure? _bbanStructure;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BankStructure? _bankStructure;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BranchStructure? _branchStructure;

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanCountry" /> class using specified 2 letter ISO region name.
        /// </summary>
        /// <param name="twoLetterISORegionName">The 2 letter iso region name.</param>
        // ReSharper disable once InconsistentNaming
        public IbanCountry(string twoLetterISORegionName)
        {
            if (twoLetterISORegionName is null)
            {
                throw new ArgumentNullException(nameof(twoLetterISORegionName));
            }

            if (twoLetterISORegionName.Length != 2)
            {
                throw new ArgumentException(Resources.ArgumentException_Invalid_country_code, nameof(twoLetterISORegionName));
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
        public IReadOnlyCollection<string> IncludedCountries { get; set; } = new ReadOnlyCollection<string>(
#if NET_LEGACY
			new string[0]
#else
            Array.Empty<string>()
#endif
        );

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
        /// Gets or sets the bank identifier structure section.
        /// </summary>
        [AllowNull]
        public BankStructure Bank
        {
            get => _bankStructure ??= new BankStructure();
            set => _bankStructure = value;
        }

        /// <summary>
        /// Gets or sets the branch identifier structure section.
        /// </summary>
        [AllowNull]
        public BranchStructure Branch
        {
            get => _branchStructure ??= new BranchStructure();
            set => _branchStructure = value;
        }

        /// <summary>
        /// Gets or sets when this <see cref="IbanCountry" /> was last updated in the Iban Registry.
        /// </summary>
        public DateTimeOffset LastUpdatedDate { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return TwoLetterISORegionName;
        }
    }
}

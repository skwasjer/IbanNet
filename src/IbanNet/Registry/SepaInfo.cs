using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IbanNet.Registry
{
    /// <summary>
    /// Represents SEPA information.
    /// </summary>
    [DebuggerStepThrough]
    public class SepaInfo
    {
        /// <summary>
        /// Gets whether this region is a SEPA country.
        /// </summary>
        public bool IsMember { get; init; }

        /// <summary>
        /// Gets a list of included SEPA countries.
        /// </summary>
        public IReadOnlyCollection<string> IncludedCountries { get; init; } = new ReadOnlyCollection<string>(
#if NET452 || NETSTANDARD1_2
			new string[0]
#else
            Array.Empty<string>()
#endif
        );
    }
}

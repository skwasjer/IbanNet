using System;

namespace IbanNet.Registry
{
    /// <summary>
    /// This IBAN registry provider contains IBAN/BBAN/SEPA information for all known IBAN countries.
    /// </summary>
    [Obsolete("The SwiftRegistryProvider moved to a new namespace IbanNet.Registry.Swift, please update your namespace imports. This facade will be removed in v5.0")]
    public class SwiftRegistryProvider : Swift.SwiftRegistryProvider
    {
    }
}

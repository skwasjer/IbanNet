using IbanNet.Registry;

namespace IbanNet.CodeGen;

/// <summary>
/// Compares <see cref="IbanCountry" /> by country code only.
/// </summary>
public sealed class IbanCountryCodeComparer : IEqualityComparer<IbanCountry>
{
    /// <inheritdoc />
    public bool Equals(IbanCountry? x, IbanCountry? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.TwoLetterISORegionName == y.TwoLetterISORegionName;
    }

    /// <inheritdoc />
    public int GetHashCode(IbanCountry obj)
    {
        return obj?.TwoLetterISORegionName.GetHashCode() ?? 0;
    }
}

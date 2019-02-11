using IbanNet.Registry;

namespace IbanNet.Validation
{
	/// <summary>
	/// Describes a factory which is used to build a validator from a defined pattern/structure.
	/// </summary>
	public interface IStructureValidationFactory
	{
		/// <summary>
		/// Creates a validator for specified <paramref name="countryInfo" /> using the provided <paramref name="structure" />.
		/// </summary>
		/// <param name="countryInfo">The country info.</param>
		/// <param name="structure">The pattern/structure to create a validator for.</param>
		/// <returns>A validator to use for the given country and structure.</returns>
		IStructureValidator CreateValidator(CountryInfo countryInfo, string structure);
	}
}
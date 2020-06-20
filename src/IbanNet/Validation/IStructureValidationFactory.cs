namespace IbanNet.Validation
{
	/// <summary>
	/// Describes a factory which is used to build a validator from a defined pattern/structure.
	/// </summary>
	public interface IStructureValidationFactory
	{
		/// <summary>
		/// Creates a validator for specified country using the provided <paramref name="pattern" />.
		/// </summary>
		/// <param name="twoLetterISORegionName">The country code.</param>
		/// <param name="pattern">The pattern to create a validator for.</param>
		/// <returns>A validator to use for the given country and structure.</returns>
		// ReSharper disable once InconsistentNaming
		IStructureValidator CreateValidator(string twoLetterISORegionName, string pattern);
	}
}

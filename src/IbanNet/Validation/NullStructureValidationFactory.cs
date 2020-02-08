namespace IbanNet.Validation
{
	/// <summary>
	/// A factory to create a validator that does not validate structures.
	/// </summary>
	public class NullStructureValidationFactory : IStructureValidationFactory
	{
		private readonly NullStructureValidator _structureValidator;

		/// <summary>
		/// Initializes a new instance of the <see cref="NullStructureValidationFactory"/>.
		/// </summary>
		public NullStructureValidationFactory()
		{
			_structureValidator = new NullStructureValidator();
		}

		/// <inheritdoc />
		public IStructureValidator CreateValidator(string twoLetterISORegionName, string structure)
		{
			return _structureValidator;
		}

		private class NullStructureValidator : IStructureValidator
		{
			public bool Validate(string iban)
			{
				return true;
			}
		}
	}
}

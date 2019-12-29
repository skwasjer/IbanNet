using System;

namespace IbanNet.Validation
{
	/// <summary>
	/// The default factory for creating a validation rule resolver.
	/// </summary>
	public class DefaultValidationRuleResolverFactory  : IValidationRuleResolverFactory
	{
		private readonly IStructureValidationFactory _structureValidationFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultValidationRuleResolverFactory"/>.
		/// </summary>
		/// <param name="structureValidationFactory">The structure validation factory.</param>
		public DefaultValidationRuleResolverFactory(IStructureValidationFactory structureValidationFactory)
		{
			_structureValidationFactory = structureValidationFactory ?? throw new ArgumentNullException(nameof(structureValidationFactory));
		}

		/// <inheritdoc />
		public IValidationRuleResolver CreateRuleResolver(IbanValidatorOptions options)
		{
			return new DefaultValidationRuleResolver(_structureValidationFactory, options?.Rules);
		}
	}
}

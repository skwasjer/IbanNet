namespace IbanNet.Validation
{
	/// <summary>
	/// A factory for creating a validation rule resolver.
	/// </summary>
	internal interface IValidationRuleResolverFactory
	{
		/// <summary>
		/// Creates a <see cref="IValidationRuleResolver"/> using specified <paramref name="options"/>.
		/// </summary>
		/// <param name="options">The validator options.</param>
		/// <returns>A new instance of <see cref="IValidationRuleResolver"/>.</returns>
		IValidationRuleResolver CreateRuleResolver(IbanValidatorOptions options);
	}
}

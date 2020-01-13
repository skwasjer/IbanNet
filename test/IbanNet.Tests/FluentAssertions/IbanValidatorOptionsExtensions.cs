namespace IbanNet.FluentAssertions
{
	public static class IbanValidatorOptionsExtensions
	{
		public static IbanValidatorOptionsAssertions Should(this IbanValidatorOptions instance)
		{
			return new IbanValidatorOptionsAssertions(instance);
		}
	}
}

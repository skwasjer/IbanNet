namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN has a country code but does not check the validity of the country code itself.
	/// </summary>
	internal class HasCountryCodeRule : RegexRule
	{
		public HasCountryCodeRule() : base(@"^\D\D")
		{
		}
	}
}
using IbanNet.Validation.Methods;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	internal class FastIbanValidatorTests : IbanValidatorIntegrationTests
	{
		public FastIbanValidatorTests()
			: base(new IbanValidator(new IbanValidatorOptions { ValidationMethod = new FastValidation() }))
		{
		}
	}
}

using System.Collections;
using IbanNet.Validation.Methods;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixtureSource(nameof(ValidatorMethodTestCases))]
	internal class FastIbanValidatorTests : IbanValidatorTests
	{
		public FastIbanValidatorTests(string fixtureName, IbanValidator validator)
			: base(fixtureName, validator)
		{
		}

		public static IEnumerable ValidatorMethodTestCases()
		{
			yield return new object[] { "Fast", new IbanValidator(new IbanValidatorOptions { ValidationMethod = new FastValidation() }) };
		}
	}
}

using System.Collections;
using System.Linq;
using IbanNet.Registry;
using NUnit.Framework;

namespace IbanNet
{
	public static class IbanTestCaseData
	{
		public static IEnumerable GetValidIbanPerCountry()
		{
			return new IbanRegistry()
				.Select(d => new TestCaseData(d.TwoLetterISORegionName, d.Iban.Example));
		}

		public static IEnumerable GetInvalidIbanPerCountry()
		{
			return new IbanRegistry()
				.Select(d => new TestCaseData(d.TwoLetterISORegionName, d.Iban.Example + "X"));
		}
	}
}
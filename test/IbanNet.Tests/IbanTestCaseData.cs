using IbanNet.Registry;

namespace IbanNet
{
    public static class IbanTestCaseData
    {
        public static IEnumerable<object[]> GetValidIbanPerCountry()
        {
            return IbanRegistry.Default
                .Select(d => new object[] { d.TwoLetterISORegionName, d.Iban.Example });
        }

        public static IEnumerable<object[]> GetInvalidIbanPerCountry()
        {
            return IbanRegistry.Default
                .Select(d => new object[] { d.TwoLetterISORegionName, d.Iban.Example + "X" });
        }
    }
}

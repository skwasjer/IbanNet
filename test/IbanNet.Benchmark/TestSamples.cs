using IbanNet.Registry;

namespace IbanNet.Benchmark;

internal static class TestSamples
{
    public static IList<string> GetIbanSamples(int count)
    {
        var generator = new IbanGenerator();
        var countryCodes = IbanRegistry.Default
            .Select(d => d.TwoLetterISORegionName)
            .ToList();

        return Enumerable.Range(0, count)
            .Select((_, index) => generator.Generate(countryCodes[index % countryCodes.Count]).ToString())
            .ToList();
    }
}

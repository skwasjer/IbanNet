using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using IbanNet.Registry;

namespace IbanNet.Benchmark;

[MarkdownExporterAttribute.GitHub]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
[MemoryDiagnoser]
public class ParserBenchmark
{
    private IbanParser _parser;

    public static IEnumerable<object[]> GetTestCases()
    {
        string iban = TestSamples.GetIbanSamples(1).Single();
        yield return new object[] { iban, false };

        string lowerIban = iban.ToLowerInvariant();
        // ReSharper disable ReplaceSubstringWithRangeIndexer
        yield return new object[] { $"{lowerIban.Substring(0, 6)} {lowerIban.Substring(6)}", true };
        // ReSharper restore ReplaceSubstringWithRangeIndexer
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _parser = new IbanParser(IbanRegistry.Default);
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetTestCases))]
    public void Parse(string iban, bool normalization)
    {
        _parser.Parse(iban);
    }
}

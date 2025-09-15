using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IbanNet.Benchmark;

[MarkdownExporterAttribute.GitHub]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
[MemoryDiagnoser]
public class ValidatorBenchmark
{
    private IbanValidator _validator;
    private IList<string> _testData;

    [Params(10000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        // IbanNet setup
        _validator = new IbanValidator();

        _testData = TestSamples.GetIbanSamples(Count);
    }

    [Benchmark(Baseline = true)]
    public void Singleton()
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        for (int i = 0; i < _testData.Count; i++)
        {
            _validator.Validate(_testData[i]);
        }
    }

    /// <summary>
    /// Not recommended: to show the difference.
    /// </summary>
    [Benchmark]
    public void Transient()
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        for (int i = 0; i < _testData.Count; i++)
        {
            var validator = new IbanValidator();
            validator.Validate(_testData[i]);
        }
    }
}

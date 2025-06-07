using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Order;
using IbanNet.CheckDigits.Calculators;

namespace IbanNet.Benchmark;

[MarkdownExporterAttribute.GitHub]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
[MemoryDiagnoser]
[InliningDiagnoser(false, ["IbanNet.Extensions", "IbanNet.CheckDigits.Calculators"])]
public class Mod97CheckDigitsCalculatorBenchmark
{
    private Mod97CheckDigitsCalculator _calculator;

    public static IEnumerable<object> TestCases()
    {
        return
        [
            new TestCase("0123456789012345"),
            new TestCase("ABCDEFGHIJKLMNOP"),
            new TestCase("01234567ABCDEFGH")
        ];
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _calculator = new Mod97CheckDigitsCalculator();
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestCases))]
    public void Test(TestCase buffer)
    {
        _calculator.Compute(buffer.Buffer);
    }

    public sealed class TestCase
    {
        private readonly string _text;

        public TestCase(string text)
        {
            _text = text;
            Buffer = text.ToCharArray();
        }

        public char[] Buffer { get; }

        public override string ToString()
        {
            return _text;
        }
    }
}

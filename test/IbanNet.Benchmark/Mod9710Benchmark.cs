using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Order;
using IbanNet.CheckDigits;

namespace IbanNet.Benchmark;

[MarkdownExporterAttribute.GitHub]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
[MemoryDiagnoser]
[InliningDiagnoser(false, ["IbanNet.Extensions", "IbanNet.CheckDigits.Calculators"])]
public class Mod9710Benchmark
{
    public static IEnumerable<object> TestCases()
    {
        return
        [
            new TestCase("0123456789012345"),
            new TestCase("ABCDEFGHIJKLMNOP"),
            new TestCase("01234567ABCDEFGH")
        ];
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestCases))]
    public void Test(TestCase buffer)
    {
        Mod9710.Compute(buffer.Buffer);
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

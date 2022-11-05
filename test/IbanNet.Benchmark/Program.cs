using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace IbanNet.Benchmark;

internal static class Program
{
    public static void Main(string[] args)
    {
        IConfig config = null;
#if DEBUG
        config = new DebugInProcessConfig();
#endif

        BenchmarkSwitcher
            .FromAssembly(typeof(Program).Assembly)
            .Run(args, config);
    }
}
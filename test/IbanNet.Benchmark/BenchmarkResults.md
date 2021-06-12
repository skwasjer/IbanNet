# IbanNet Benchmark Results

## Release 4.4

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.985 (2004/?/20H1)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host]     : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT
  Job-JZWPKX : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT
  Job-USMEXX : .NET Core 3.1.15 (CoreCLR 4.700.21.21202, CoreFX 4.700.21.21402), X64 RyuJIT
  Job-BHSBZT : .NET Core 2.1.28 (CoreCLR 4.6.30015.01, CoreFX 4.6.30015.01), X64 RyuJIT
  Job-SQNNCW : .NET Framework 4.8 (4.8.4341.0), X64 RyuJIT
```

### Strict vs Loose validation

|   Method |        Job |       Runtime |    Toolchain |      validator |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------- |-------------- |------------- |--------------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| Validate | Job-JZWPKX | .NET Core 5.0 | netcoreapp50 |  IbanNet Loose | 476.5 ns |  2.24 ns |  1.98 ns |  1.00 |    0.00 | 0.1097 |     - |     - |     688 B |
| Validate | Job-USMEXX | .NET Core 3.1 | netcoreapp31 |  IbanNet Loose | 567.7 ns |  2.85 ns |  2.66 ns |  1.19 |    0.01 | 0.1097 |     - |     - |     688 B |
| Validate | Job-BHSBZT | .NET Core 2.1 | netcoreapp21 |  IbanNet Loose | 608.5 ns |  4.72 ns |  4.42 ns |  1.28 |    0.01 | 0.1097 |     - |     - |     696 B |
| Validate | Job-SQNNCW |      .NET 4.8 |        net48 |  IbanNet Loose | 678.1 ns |  3.21 ns |  3.00 ns |  1.42 |    0.01 | 0.0925 |     - |     - |     586 B |
|          |            |               |              |                |          |          |          |       |         |        |       |       |           |
| Validate | Job-JZWPKX | .NET Core 5.0 | netcoreapp50 | IbanNet Strict | 637.0 ns |  8.46 ns |  7.50 ns |  1.00 |    0.00 | 0.1249 |     - |     - |     784 B |
| Validate | Job-USMEXX | .NET Core 3.1 | netcoreapp31 | IbanNet Strict | 727.6 ns |  3.85 ns |  3.41 ns |  1.14 |    0.02 | 0.1249 |     - |     - |     784 B |
| Validate | Job-BHSBZT | .NET Core 2.1 | netcoreapp21 | IbanNet Strict | 800.7 ns | 13.82 ns | 12.93 ns |  1.26 |    0.03 | 0.1249 |     - |     - |     792 B |
| Validate | Job-SQNNCW |      .NET 4.8 |        net48 | IbanNet Strict | 871.5 ns | 16.78 ns | 13.10 ns |  1.37 |    0.03 | 0.1078 |     - |     - |     682 B |

### Comparison with other validators

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.985 (2004/?/20H1)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host]     : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT
  Job-TRNOOX : .NET Core 3.1.15 (CoreCLR 4.700.21.21202, CoreFX 4.700.21.21402), X64 RyuJIT
  Job-PGOFTL : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT
  Job-YWTJNJ : .NET Core 2.1.28 (CoreCLR 4.6.30015.01, CoreFX 4.6.30015.01), X64 RyuJIT
  Job-ZMNWCT : .NET Framework 4.8 (4.8.4341.0), X64 RyuJIT
```

|                    Method |        Job |       Runtime |    Toolchain | Count |        Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |----------- |-------------- |------------- |------ |------------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             IbanNet_Loose | Job-PGOFTL | .NET Core 5.0 | netcoreapp50 |    10 |  6,154.3 ns |  42.31 ns |    37.51 ns |  0.83 |    0.01 | 1.1215 |     - |     - |    7056 B |
|             IbanNet_Loose | Job-TRNOOX | .NET Core 3.1 | netcoreapp31 |    10 |  6,221.6 ns |  22.44 ns |    19.90 ns |  0.84 |    0.01 | 1.1215 |     - |     - |    7056 B |
|             IbanNet_Loose | Job-YWTJNJ | .NET Core 2.1 | netcoreapp21 |    10 |  6,692.0 ns |  40.28 ns |    35.71 ns |  0.91 |    0.01 | 1.1292 |     - |     - |    7112 B |
| IbanNet_Strict_CacheReuse | Job-PGOFTL | .NET Core 5.0 | netcoreapp50 |    10 |  6,938.7 ns | 136.96 ns |   192.00 ns |  0.95 |    0.03 | 1.2436 |     - |     - |    7840 B |
|             IbanNet_Loose | Job-ZMNWCT |      .NET 4.8 |        net48 |    10 |  7,087.1 ns | 138.30 ns |   135.83 ns |  0.96 |    0.02 | 0.9613 |     - |     - |    6058 B |
| IbanNet_Strict_CacheReuse | Job-TRNOOX | .NET Core 3.1 | netcoreapp31 |    10 |  7,312.7 ns |  49.40 ns |    43.79 ns |  0.99 |    0.01 | 1.2436 |     - |     - |    7840 B |
|            IbanNet_Strict | Job-PGOFTL | .NET Core 5.0 | netcoreapp50 |    10 |  7,372.2 ns |  98.76 ns |    77.10 ns |  1.00 |    0.00 | 1.2741 |     - |     - |    8016 B |
|            IbanNet_Strict | Job-TRNOOX | .NET Core 3.1 | netcoreapp31 |    10 |  7,691.1 ns |  26.97 ns |    22.53 ns |  1.04 |    0.01 | 1.2665 |     - |     - |    8016 B |
| IbanNet_Strict_CacheReuse | Job-YWTJNJ | .NET Core 2.1 | netcoreapp21 |    10 |  7,984.6 ns |  28.90 ns |    22.56 ns |  1.08 |    0.01 | 1.2512 |     - |     - |    7920 B |
|            IbanNet_Strict | Job-YWTJNJ | .NET Core 2.1 | netcoreapp21 |    10 |  8,454.6 ns |  38.04 ns |    31.76 ns |  1.15 |    0.01 | 1.2817 |     - |     - |    8072 B |
| IbanNet_Strict_CacheReuse | Job-ZMNWCT |      .NET 4.8 |        net48 |    10 |  8,483.5 ns |  32.29 ns |    26.97 ns |  1.15 |    0.01 | 1.0834 |     - |     - |    6820 B |
|            IbanNet_Strict | Job-ZMNWCT |      .NET 4.8 |        net48 |    10 |  9,001.0 ns | 137.47 ns |   209.93 ns |  1.23 |    0.04 | 1.1139 |     - |     - |    7021 B |
|       NuGet_IbanValidator | Job-PGOFTL | .NET Core 5.0 | netcoreapp50 |    10 | 27,928.8 ns | 226.90 ns |   201.14 ns |  3.79 |    0.05 | 6.0730 |     - |     - |   38112 B |
|       NuGet_IbanValidator | Job-TRNOOX | .NET Core 3.1 | netcoreapp31 |    10 | 32,962.9 ns | 578.62 ns |   483.17 ns |  4.47 |    0.09 | 6.1646 |     - |     - |   38912 B |
|            NuGet_IBAN4NET | Job-PGOFTL | .NET Core 5.0 | netcoreapp50 |    10 | 35,207.1 ns | 534.17 ns |   499.66 ns |  4.75 |    0.09 | 1.8311 |     - |     - |   11592 B |
|       NuGet_IbanValidator | Job-YWTJNJ | .NET Core 2.1 | netcoreapp21 |    10 | 37,544.1 ns | 160.19 ns |   149.84 ns |  5.09 |    0.06 | 6.2256 |     - |     - |   39400 B |
|            NuGet_IBAN4NET | Job-TRNOOX | .NET Core 3.1 | netcoreapp31 |    10 | 37,687.2 ns | 212.29 ns |   177.27 ns |  5.11 |    0.05 | 1.8311 |     - |     - |   11592 B |
|       NuGet_IbanValidator | Job-ZMNWCT |      .NET 4.8 |        net48 |    10 | 43,333.1 ns | 439.69 ns |   389.78 ns |  5.87 |    0.06 | 5.0049 |     - |     - |   31549 B |
|            NuGet_IBAN4NET | Job-ZMNWCT |      .NET 4.8 |        net48 |    10 | 51,977.6 ns | 867.16 ns | 1,186.98 ns |  7.08 |    0.19 | 2.0752 |     - |     - |   13689 B |
|            NuGet_IBAN4NET | Job-YWTJNJ | .NET Core 2.1 | netcoreapp21 |    10 | 52,996.8 ns | 248.96 ns |   207.90 ns |  7.19 |    0.09 | 1.8921 |     - |     - |   12208 B |

### CLI

To run the benchmark:
```
cd ./test/IbanNet.Benchmark
dotnet run -c Release -f net5.0 netcoreapp3.1 netcoreapp2.1 net48 --runtimes netcoreapp50 netcoreapp31 netcoreapp21 net48
```

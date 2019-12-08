# Increasing performance - phase 1

Below the iterations in improving performance and memory footprint of IbanNet.

The intermediate steps only list measurements where they are appropriate.

### Baseline (<= v3.2)

|           Validator |       Mean |     Error |    StdDev |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------------- |-----------:|----------:|----------:|--------:|-------:|------:|----------:|
| NuGet IbanValidator v2.0.0|   3.060 us | 0.0475 us | 0.0444 us |  0.4807 |      - |     - |   2.97 KB |
|             IbanNet v3.2.0 |   5.587 us | 0.0489 us | 0.0433 us |  0.5493 |      - |     - |   3.41 KB |
|      NuGet IBAN4NET v1.0.6 | 607.935 us | 2.7713 us | 2.5923 us | 15.6250 | 0.9766 |     - |  97.97 KB |

- NuGet IbanValidator performs pretty good. Note that it does not do strict validation, so false positives may occur.
- **IbanNet** performance is decent, but does do strict validation. This means no false positives, at cost of extra CPU time.
- NuGet IBAN4NET does not perform very good because it recreates a list of countries per individual validation.

As the author of IbanNet, I can attest that code readability was given priority over performance. Let's see if we can change this.

### After refactor (no regex rules)

|           Validator |       Mean |      Error |     StdDev |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------------- |-----------:|-----------:|-----------:|--------:|-------:|------:|----------:|
|        IbanNet Fast |   3.099 us |  0.0525 us |  0.0465 us |  0.5264 |      - |     - |   3.25 KB |
|      IbanNet Strict |   3.821 us |  0.0642 us |  0.0570 us |  0.5531 |      - |     - |   3.41 KB |

- I removed use of regex from 3 validation rules.
- I added a 'Fast' validator mode, which does the same type of validation as NuGet IbanValidator, which we're now already on par with.

### Faster Mod97

|           Validator |       Mean |     Error |    StdDev |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------------- |-----------:|----------:|----------:|--------:|-------:|------:|----------:|
|        IbanNet Fast |   2.585 us | 0.0341 us | 0.0302 us |  0.4120 |      - |     - |   2.55 KB |
|      IbanNet Strict |   3.207 us | 0.0538 us | 0.0449 us |  0.4387 |      - |     - |   2.71 KB |

- Refactor the Mod97 rule, removing use of LINQ, string join/concatenation and string parsing.
- 'Strict' mode performance now comes close to NuGet IbanValidator.

### Structure validation without regex

|           Validator |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
| IbanNet Strict | 2.753 us | 0.0299 us | 0.0280 us | 0.4463 |     - |     - |   2.74 KB |

- Refactor the structure validation so that we no longer use regex. This has no gains for 'Fast' mode, but 'Strict' mode now is a winner.

### Normalization without regex

|           Validator |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
|   IbanNet Fast | 2.273 us | 0.0095 us | 0.0079 us | 0.4349 |     - |     - |    2.7 KB |
| IbanNet Strict | 2.473 us | 0.0027 us | 0.0022 us | 0.4692 |     - |     - |   2.89 KB |

- Final optimization, replacing the regex that is used to remove whitespace with char buffer iteration.

### Environment

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), X64 RyuJIT
  DefaultJob : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), X64 RyuJIT

```

## Conclusion

- No more use of regex.
- 'Strict' performance over twice as fast.
- Lowered memory footprint a bit.
- 'Strict' and 'Fast' are now very close, both being faster than any other available NuGet package.

# Increasing performance - phase 2

Still room for improvements, let's see what we can do.

### New baseline

|      validator |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
|   IbanNet Fast | 2.325 us | 0.0164 us | 0.0153 us | 0.4463 |     - |     - |   2.75 KB |
| IbanNet Strict | 2.513 us | 0.0055 us | 0.0051 us | 0.4768 |     - |     - |   2.95 KB |

- First set new baseline, some new functionality was added which comes with a very minor performance penalty.

### Use `ulong` primarily and only use much slower `BigInteger` for final Mod97 calculation

|      validator |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|   IbanNet Fast | 535.2 ns |  2.41 ns |  2.25 ns | 0.1078 |     - |     - |     688 B |
| IbanNet Strict | 694.8 ns | 11.83 ns | 10.49 ns | 0.1345 |     - |     - |     856 B |

- Most time is spent in Mod97 calculation due to necessity for big integer. But with some cleverness we can actually do most computations using `ulong`, and only do final Mod97 calculation using `BigInteger`. This comes at cost of code readability, but the performance gain is significant though, and more than I hoped for.

## Using custom integer power func

|   validator |     Mean |   Error |  StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|--------:|--------:|-------:|------:|------:|----------:|
|   IbanNet Fast | 512.6 ns | 2.24 ns | 2.10 ns | 0.1097 |     - |     - |     688 B |
| IbanNet Strict | 652.1 ns | 8.56 ns | 7.15 ns | 0.1364 |     - |     - |     856 B |

- In last refactor we use `Math.Pow` but with a simple and efficient integer power function we can get some performance increase.

## Conclusion

Now we're cooking. There's probably still optimizations possible (perhaps using `Span<T>`), but I am happy enough with it the current result.

- Strict validation is almost 9x faster.
- Memory footprint is 4x lower.

It just goes to show what abstractions can do for you in terms of code readability and maintenance, but that it generally comes with some performance penalty.

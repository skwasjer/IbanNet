# IbanNet <img align="right" width="64" height="64" src="IbanNet64.png">

IbanNet is a .NET library providing functionality to validate and parse an [International Bank Account Number](https://en.wikipedia.org/wiki/International_Bank_Account_Number) also known as IBAN. 

Additionally, IbanNet provides:
- the `Iban` primitive type, which can be used as a drop in replacement for a `string` in your domain. 
- a builder to construct IBAN's from a Basic Bank Account Number (BBAN).
- a generator to assist with (unit) testing.

---

[![Build status](https://ci.appveyor.com/api/projects/status/469oo89bngrkgh2l/branch/main?svg=true)](https://ci.appveyor.com/project/skwasjer/ibannet)
[![Tests](https://img.shields.io/appveyor/tests/skwasjer/IbanNet/main.svg)](https://ci.appveyor.com/project/skwasjer/ibannet/build/tests)
[![codecov](https://codecov.io/gh/skwasjer/IbanNet/branch/main/graph/badge.svg)](https://codecov.io/gh/skwasjer/IbanNet)

| | | |
|---|---|---|
| `IbanNet` | [![NuGet](https://img.shields.io/nuget/v/IbanNet.svg)](https://www.nuget.org/packages/IbanNet/) [![NuGet](https://img.shields.io/nuget/dt/IbanNet.svg)](https://www.nuget.org/packages/IbanNet/) | [Documentation](../../wiki)
| `IbanNet.DataAnnotations` | [![NuGet](https://img.shields.io/nuget/v/IbanNet.DataAnnotations.svg)](https://www.nuget.org/packages/IbanNet.DataAnnotations/) [![NuGet](https://img.shields.io/nuget/dt/IbanNet.DataAnnotations.svg)](https://www.nuget.org/packages/IbanNet.DataAnnotations/) | [Documentation](../../wiki/IbanNet.DataAnnotations) |
| `IbanNet.DependencyInjection.Autofac` | [![NuGet](https://img.shields.io/nuget/v/IbanNet.DependencyInjection.Autofac.svg)](https://www.nuget.org/packages/IbanNet.DependencyInjection.Autofac/) [![NuGet](https://img.shields.io/nuget/dt/IbanNet.DependencyInjection.Autofac.svg)](https://www.nuget.org/packages/IbanNet.DependencyInjection.Autofac/) | [Documentation](../../wiki/Dependency-injection) |
| `IbanNet.DependencyInjection.ServiceProvider` | [![NuGet](https://img.shields.io/nuget/v/IbanNet.DependencyInjection.ServiceProvider.svg)](https://www.nuget.org/packages/IbanNet.DependencyInjection.ServiceProvider/) [![NuGet](https://img.shields.io/nuget/dt/IbanNet.DependencyInjection.ServiceProvider.svg)](https://www.nuget.org/packages/IbanNet.DependencyInjection.ServiceProvider/) | [Documentation](../../wiki/Dependency-injection) |
| `IbanNet.FluentValidation` | [![NuGet](https://img.shields.io/nuget/v/IbanNet.FluentValidation.svg)](https://www.nuget.org/packages/IbanNet.FluentValidation/) [![NuGet](https://img.shields.io/nuget/dt/IbanNet.FluentValidation.svg)](https://www.nuget.org/packages/IbanNet.FluentValidation/) | [Documentation](../../wiki/IbanNet.FluentValidation) |


## Example with validator

```csharp
IIbanValidator validator = new IbanValidator();
ValidationResult validationResult = validator.Validate("NL91 ABNA 0417 1643 00");
if (validationResult.IsValid)
{
    // ..
}
```

## Example with `Iban` type

```csharp
Iban iban;
IIbanParser parser = new IbanParser(IbanRegistry.Default);
bool success = parser.TryParse("NL91 ABNA 0417 1643 00", out iban);
if (success)
{
    Console.WriteLine(iban.ToString(IbanFormat.Obfuscated)); // XXXXXXXXXXXXXX4300
}
```

### Contributions

Please check out the [contribution guidelines](./CONTRIBUTING.md).

### Other info

- [Changelog](./CHANGELOG.md)
- [IbanNet supported countries](SupportedCountries.md)
- [Fiddle](https://dotnetfiddle.net/JeGa9x)

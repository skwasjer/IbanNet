# IbanNet

IbanNet is a .NET library providing functionality to validate and parse an [International Bank Account Number](https://en.wikipedia.org/wiki/International_Bank_Account_Number) also known as IBAN.

## Installation

Install IbanNet via the Nuget package manager or `dotnet` cli.

```powershell
Install-Package IbanNet
```
For Data Annotation integration:
```powershell
Install-Package IbanNet.DataAnnotations
```
For FluentValidation integration:
```powershell
Install-Package IbanNet.FluentValidation
```

---

[![Build status](https://ci.appveyor.com/api/projects/status/469oo89bngrkgh2l/branch/master?svg=true)](https://ci.appveyor.com/project/skwasjer/ibannet)
[![Tests](https://img.shields.io/appveyor/tests/skwasjer/IbanNet/master.svg)](https://ci.appveyor.com/project/skwasjer/ibannet/build/tests)
[![codecov](https://codecov.io/gh/skwasjer/IbanNet/branch/master/graph/badge.svg)](https://codecov.io/gh/skwasjer/IbanNet)

| | | |
|---|---|---|
| `IbanNet` | [![NuGet](https://img.shields.io/nuget/v/IbanNet.svg)](https://www.nuget.org/packages/IbanNet/) [![NuGet](https://img.shields.io/nuget/dt/IbanNet.svg)](https://www.nuget.org/packages/IbanNet/) | [Documentation](src/IbanNet/README.md)
| `IbanNet.DataAnnotations` | [![NuGet](https://img.shields.io/nuget/v/IbanNet.DataAnnotations.svg)](https://www.nuget.org/packages/IbanNet.DataAnnotations/) [![NuGet](https://img.shields.io/nuget/dt/IbanNet.DataAnnotations.svg)](https://www.nuget.org/packages/IbanNet.DataAnnotations/) | [Documentation](src/IbanNet.DataAnnotations/README.md) |
| `IbanNet.FluentValidation` | [![NuGet](https://img.shields.io/nuget/v/IbanNet.FluentValidation.svg)](https://www.nuget.org/packages/IbanNet.FluentValidation/) [![NuGet](https://img.shields.io/nuget/dt/IbanNet.FluentValidation.svg)](https://www.nuget.org/packages/IbanNet.FluentValidation/) | [Documentation](src/IbanNet.FluentValidation/README.md) |

## Example with validator

```csharp
IIbanValidator validator = new IbanValidator();
ValidationResult validationResult = validator.Validate("NL91ABNA041716430");
if (validationResult.IsValid) {
  // For example:
  if (validationResult.Country.TwoLetterISORegionName != "NL") {
    throw new InvalidOperationException("Please provide a Dutch bank account.");
  }
}
```

## Example with `Iban` type

```csharp
Iban iban;
bool success = Iban.TryParse("NL91 ABNA 0417 1643 00", out iban);
```

### Contributions
PR's are welcome. Please rebase before submitting, provide test coverage, and ensure the AppVeyor build passes. I will not consider PR's otherwise.

### Contributors
- skwas (author/maintainer)

### Useful info

- [Changelog](Changelog.md)
- [IbanNet supported countries](SupportedCountries.md)
- [Fiddle](https://dotnetfiddle.net/JeGa9x)
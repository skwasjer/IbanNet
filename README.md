[![Build status](https://ci.appveyor.com/api/projects/status/469oo89bngrkgh2l?svg=true)](https://ci.appveyor.com/project/skwasjer/ibannet)
[![NuGet](https://img.shields.io/nuget/v/IbanNet.svg)](https://www.nuget.org/packages/IbanNet/)
[![Tests](https://img.shields.io/appveyor/tests/skwasjer/IbanNet.svg)](https://ci.appveyor.com/project/skwasjer/ibannet/build/tests)

# IbanNet

IbanNet is a .NET library providing functionality to validate and parse an [International Bank Account Number](https://en.wikipedia.org/wiki/International_Bank_Account_Number) also known as IBAN.

## Usage

#### Parse
The simplest way to use this library is to use the static `Parse` method:

```csharp
Iban iban = Iban.Parse("NL91ABNA0417164300");
```

When parsing fails, an `IbanFormatException` is thrown, which contains a property Result which indicates the type of error that occurred.

#### TryParse

To parse an IBAN without throwing an exception use `TryParse`.

```csharp
Iban iban;
bool success = Iban.TryParse("NL91ABNA0417164300", out iban);
```

The only downside is there is no way to retrieve the type of validation error that occurred if parsing fails.

#### IbanValidator

Lastly, you can use the validator directly. The benefit of using the validator is that it implements the `IIbanValidator` interface and can thus be mocked. Additionally, the `ValidationResult` provides extra context, like the matched country (if any).

For example:

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

### Iban type

`Iban.Parse` and `Iban.TryParse` return a type Iban. The Iban type is entirely optional, and if you use the validator directly you won't ever need to use this type.

However, this type does provide some useful features:

#### Format IBAN
To convert an Iban type to a string, call the `ToString(string format)` method with either of 2 formats:

|Format|Result|
|---|---|
|"F" or `IbanNet.Iban.Formats.Flat`|NL91ABNA0417164300|
|"S" or `IbanNet.Iban.Formats.Partitioned`|NL91 ABNA 0417 1643 00|

If `ToString()` is called without format, defaults to 'F'.

#### Equals

Whether you parsed `NL91 ABNA 0417 1643 00` or `NL91ABNA0417164300`, these IBAN are considered equal.

#### Mocking the static Parse/TryParse methods

If you use the static methods because you would like to use the Iban type, you can still mock the underlying validator for testing purposes by setting the static property `Iban.Validator`.

## Installation

Install **IbanNet** via [NuGet](https://www.nuget.org/packages/IbanNet/).

### Visual Studio / .NET Framework
```powershell
Install-Package IbanNet
```

### .NET CLI
```
dotnet add package IbanNet
```

### Supported .NET targets
- .NET 4.5 or greater
- .NET Standard 1.2 or greater

### Requirements
- Visual Studio 2017
- .NET Core 2.0 SDK

#### Contributions
PR's are welcome. Please rebase before submitting, provide test coverage, and ensure the AppVeyor build passes. I will not consider PR's otherwise.

#### Contributors
- skwas (author/maintainer)

## Useful info

- [Changelog](Changelog.md)
- [IbanNet supported regions](SupportedRegions.md)
- [Fiddle](https://dotnetfiddle.net/JeGa9x)
- Related/associated libraries
  - [IbanNet.DataAnnotations](src/IbanNet.DataAnnotations/README.md)

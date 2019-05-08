[![Build status](https://ci.appveyor.com/api/projects/status/469oo89bngrkgh2l?svg=true)](https://ci.appveyor.com/project/skwasjer/ibannet)
[![NuGet](https://img.shields.io/nuget/v/IbanNet.FluentValidation.svg)](https://www.nuget.org/packages/IbanNet.FluentValidation/)
[![Tests](https://img.shields.io/appveyor/tests/skwasjer/IbanNet.svg)](https://ci.appveyor.com/project/skwasjer/ibannet/build/tests)

# IbanNet.FluentValidation

[FluentValidation](https://fluentvalidation.net/) support to validate IBAN user input.

## Introduction

This library provides a property validator for the [FluentValidation](https://fluentvalidation.net/) framework that can be used in your rules to verify IBAN user input.

## Usage

Set up your rule.

### Example ###

```csharp
public class InputModel
{	
    public string BackAccountNumber { get; set; }
}
```

Create a new validator.

```csharp
using FluentValidation;
using IbanNet;
using IbanNet.FluentValidation;

public class InputModelValidator : AbstractValidator<InputModel>
{
    public InputModelValidator(IIbanValidator ibanValidator)
    {
        RuleFor(x => x.BankAccountNumber).Iban(ibanValidator);
    }
}
```

## Dependency injection

You can use your favorite DI provider to provide an [`IIbanValidator`](../IbanNet/IIbanValidator.cs) to the validator.

### .NET Core example ###

```csharp
services.AddSingleton<IIbanValidator, IbanValidator>();
services.AddTransient<IValidator<InputModel>, InputModelValidator>()
services.AddFluentValidation();
```

## Installation

Install **IbanNet.FluentValidation** via [NuGet](https://www.nuget.org/packages/IbanNet.FluentValidation/).

#### Visual Studio / .NET Framework
```powershell
Install-Package IbanNet.FluentValidation
```

#### .NET CLI
```
dotnet add package IbanNet.FluentValidation
```

#### Dependencies

- FluentValidation 8.x

### .NET target frameworks
- .NET Standard 2.0
- .NET Standard 1.6
- .NET Standard 1.2
- .NET 4.7
- .NET 4.5

### Build requirements
- Visual Studio 2017
- .NET Core 2.1 SDK
- .NET Core 2.0 SDK
- .NET Core 1.1 SDK
- .NET 4.7 targetting pack
- .NET 4.5 targetting pack

#### Contributions
PR's are welcome. Please rebase before submitting, provide test coverage, and ensure the AppVeyor build passes. I will not consider PR's otherwise.

#### Contributors
- skwas (author/maintainer)

## Related libraries
- [IbanNet](../../README.md)
- [IbanNet.DataAnnotations](../IbanNet.DataAnnotations/README.md)
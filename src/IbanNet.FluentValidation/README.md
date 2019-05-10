# IbanNet.FluentValidation

[FluentValidation](https://fluentvalidation.net/) support to validate IBAN user input.

[Package installation](../../README.md)

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

## Requirements

### Dependencies

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
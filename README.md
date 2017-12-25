[![Build status](https://ci.appveyor.com/api/projects/status/469oo89bngrkgh2l?svg=true)](https://ci.appveyor.com/project/skwasjer/ibannet)

# IbanNet

IbanNet is a .NET library providing functionality to validate and parse an [International Bank Account Number](https://en.wikipedia.org/wiki/International_Bank_Account_Number) also known as IBAN.

## Usage

#### Parse
The simplest way to use this library is to use the static Parse method:

```csharp
Iban iban = Iban.Parse("NL91ABNA0417164300");
```

When parsing fails, a IbanFormatException is thrown, which contains a property Result which indicates the type of error that occurred.

#### TryParse

To parse an IBAN without throwing an exception use TryParse.

```csharp
Iban iban;
bool success = Iban.TryParse("NL91ABNA0417164300", out iban);
```

The only downside is there is no way to retrieve the type of validation error that occurred if parsing fails.

#### IbanValidator

Lastly, you can use the validator directly:
```csharp
IIbanValidator validator = new IbanValidator();
IbanValidationResult validationResult = validator.Validate("NL91ABNA041716430");
```
The benefit of using the validator is that it implements the IIbanValidator interface. This allows for mocking and using a DI container. It is recommended that when you do, that you cache the validator or register it as a singleton, since the initialization - although not much - does introduce some overhead when initializing the IBAN validation rules.

## Installation

Install **IbanNet** via [NuGet](https://www.nuget.org/packages/IbanNet/).

#### Visual Studio / .NET Framework
```powershell
Install-Package IbanNet
```

#### .NET CLI
```
dotnet add package IbanNet
```

### Future enhancements
- Add support for conversion from BBAN to IBAN via 3rd party API's.

#### Supported .NET targets
- .NET 4.5.2
- .NET 4.6.2
- .NET Standard 1.2

#### Requirements
- Visual Studio 2017
- .NET Core 2.0 SDK

#### Contributions
Since this is a new package I'm still working on CI, refactoring and perhaps some restructuring. PR's are welcome but keep this in mind, as it may cause me to decline PR's if upcoming changes may conflict too much with PR's.

#### Contributors
- skwas (author/maintainer)

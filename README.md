[![Build status](https://ci.appveyor.com/api/projects/status/469oo89bngrkgh2l?svg=true)](https://ci.appveyor.com/project/skwasjer/ibannet)

# IbanNet

IbanNet is a .NET library providing functionality to validate and parse an [International Bank Account Number](https://en.wikipedia.org/wiki/International_Bank_Account_Number) also known as IBAN.

## Usage

```csharp
Iban iban = Iban.Parse("NL91ABNA0417164300");
```
or

```csharp
Iban iban;
bool success = Iban.TryParse("NL91ABNA0417164300", out iban);
```

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
## API

TODO: ...

### Future enhancements
- Change to .NET Standard 1.x
- Add support for conversion from BBAN to IBAN via 3rd party API's.

#### Supported platforms
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

[![Build status](https://ci.appveyor.com/api/projects/status/469oo89bngrkgh2l?svg=true)](https://ci.appveyor.com/project/skwasjer/ibannet)

# IbanNet

IbanNet is a .NET library providing functionality to validate an [International Bank Account Number](https://en.wikipedia.org/wiki/International_Bank_Account_Number) also known as IBAN.

## Requirements

- .NET 4.5.2
- Visual Studio 2015

## Usage

```csharp
Iban iban = Iban.Parse("NL91ABNA0417164300");
```
or

```csharp
Iban iban;
bool success = Iban.TryParse("NL91ABNA0417164300", out iban);
```

## API

TODO: ...

## Future enhancements
- Change to .NET Standard 1.x
- Add support for conversion from BBAN to IBAN via 3rd party API's.

## Contributions

TODO: ...

### Contributors
- skwas (author/maintainer)

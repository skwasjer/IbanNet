IbanNet is a .NET library providing functionality to validate and parse an [International Bank Account Number](https://en.wikipedia.org/wiki/International_Bank_Account_Number) also known as IBAN. 

Additionally, IbanNet provides:
- the `Iban` primitive type, which can be used as a drop in replacement for a `string` in your domain. 
- a builder to construct IBAN's from a Basic Bank Account Number (BBAN).
- a generator to assist with (unit) testing.

## Example with validator

```csharp
IIbanValidator validator = new IbanValidator();
ValidationResult validationResult = validator.Validate("NL91ABNA0417164300");
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

## Other info

- [Changelog](https://github.com/skwasjer/IbanNet/blob/main/CHANGELOG.md)
- [IbanNet supported countries](https://github.com/skwasjer/IbanNet/blob/main/SupportedCountries.md)
- [Fiddle](https://dotnetfiddle.net/JeGa9x)

### Contributions

Please check out the [contribution guidelines](https://github.com/skwasjer/IbanNet/blob/main/CONTRIBUTING.md).

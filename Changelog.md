# Changelog

## v3.1.1

- Removed ability to apply `IbanAttribute` to fields, since model validation does not occur for fields.
- Improved continuous integration, added code coverage.

## v3.1

- Deprecated `IbanNet.IbanTypeConverter`, replaced by  `IbanNet.TypeConverters.IbanTypeConverter`.
- Added [IbanNet.FluentValidation](src/IbanNet.FluentValidation/README.md)  package.
- Upgraded registry to April '19 release 83.
- Added extra target frameworks `.NET 4.7`, `.NET Standard 1.6` and `.NET Standard 2.0`

## v3.0

- Partial rewrite to support the official [Swift IBAN registry](https://www.swift.com/standards/data-standards/iban).
- Added support for 4 more countries for a total of 76.
- Added details through `CountryInfo`, including IBAN, BBAN, bank and branch structure information, whether a country is a SEPA member and more.
- (breaking) Replaced `IbanValidator.SupportedRegions` with `IbanValidator.SupportedCountries`.
- (breaking) The `IIbanValidator.Validate` method now returns a `ValidationResult` object, instead of an enum value, in order to provide more information of the validation.

## v2.1

- Added [IbanNet.DataAnnotations](src/IbanNet.DataAnnotations/README.md) for DataAnnotation support.

## v2.0

- (breaking) retarget from .NET Framework 4.5.2 to .NET 4.5.
- Added `TypeConverter` support.

## v1.2

- Expose supported regions via `IbanValidator.SupportedRegions`

## v1.1

- Convert to .NET Standard 1.2

## v1.0

- Initial release with support for 72 countries

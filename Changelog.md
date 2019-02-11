# Changelog

## v3.0-rc

- Partial rewrite to support the official [Swift IBAN registry](https://www.swift.com/standards/data-standards/iban).
- Added support for 4 more countries for a total of 76.
- Added details through `CountryInfo`, including IBAN, BBAN, bank and branch structure information, whether a country is a SEPA member and more.
- (breaking) Deprecated `IbanValidator.SupportedRegions` in favor of the new `IbanValidator.SupportedCountries`.

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
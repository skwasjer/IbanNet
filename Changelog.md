# Changelog

## v4.0.0

### Improvements

- Added `IbanParser` which provides equivalent non-static functionality to `Iban.Parse` and `Iban.TryParse` (which will be obsolete).
- Added .NET Standard 2.1 target
- Enabled and refactored for non-nullable reference types.
- Added abstraction to load registry from different sources.
- Added `ICheckDigitsCalculator` abstraction.
- Exposing `IIbanValidationRule` allowing custom validation rules.
- [Performance improvements](test/IbanNet.Benchmark/BenchmarkResults.md)

### Changes

- (breaking) `Iban.Parse` and `Iban.TryParse` are obsolete, use `IIbanParser`.
- (breaking) Added IbanValidator ctor overload accepting an `IbanValidatorOptions` class, providing options with validation method (strict = default vs loose), extensibility through custom rules.
- (breaking) Refactored out enum `IbanValidationResult`, replaced with result object pattern for extensibility.
- (breaking) `ValidationResult` now contains `Error`-property containing the error that occurred.
- (breaking) Remove deprecated TypeConverter facade.
- (breaking) Remove deprecated ctor (accepting `Lazy`).
- (breaking) `IbanValidator.SupportedCountries` now is a dictionary, allowing look up by country code.
- (breaking) Renamed `CountryInfo` to `IbanCountry`.
- (breaking) Renamed `ValidationResult.Value` to `ValidationResult.AttemptedValue`.
- (breaking) Moved `Branch` and `Bank` properties from `BbanStructure` to `IbanCountry` and all offsets are now relative to entire IBAN. This makes it easier to extract this data from an IBAN.
- (breaking) `IbanAttribute` no longer uses static `Iban.Validator` as fallback since it is unclear it does so. The `IIbanValidator` will now only be resolved from IoC container and as such if not registered an exception is thrown.

## v3.2.1

- Upgraded registry to Januari '20 release 85 (improves Egypt, not effective until 2021).

## v3.2.0

- Upgraded registry to October '19 release 84 (adds Egypt).

## v3.1.2

- Enabled [SourceLink](https://github.com/dotnet/sourcelink).

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

# Changelog

## v5.9.0

- Refactored pattern tokenizer to support wide tokens (multi char), next to (single) char tokens. This is an internal change that can unlock some extra future functionality.

## v5.8.1

- Upgraded SWIFT registry to Feb '23 release 93, which adds Somalia (SO) and fixes some misconfiguration for Finland (FI) removing the need for 'some' patching (no regression).
- Updated Wikipedia provider, which updates validation patterns for Dominican Republic (DO), Georgia (GE), Ireland (IE), Jordan (JO), Pakistan (PK), Palestinian territories (PS), Turkey (TR), British Virgin Islands (VG) and Burundi (BI).

## v5.8.0

- Update `FluentValidation` to v11.3.0
- Updated Wikipedia registry provider which corrects the IBAN and BBAN pattern for Azerbaijan (AZ). The Swift registry (which is the default) was already correct, so there is no regression if you do not use the Wikipedia provider.
- [#116](https://github.com/skwasjer/IbanNet/pull/116) Added .NET 7 target framework support
- Performance improvements (10-20%):
  - Use `CollectionsMarshal.AsSpan` in some areas to improve enumeration perf
  - Use faster API's to change a `Span<char>` back to a `string`
  - Use new .NET 7 `char` API's and aggressive inline own `char` extensions
  - Rework `Pattern` lazy init to improve init time
  - When parsing, do not allocate and return a new copy of the input if no normalization was performed.

## v5.7.2

- Lazy initialization of `IbanRegistry` is now thread safe.
- Add ctor overload to `IbanGenerator` which enables specifying a seed for consistent, reproduceable generation.
- Enable deterministic build/assemblies

## v5.7.1

- [#97](https://github.com/skwasjer/IbanNet/pull/97) Add Dutch localized exception/result messages.

## v5.7.0

- Move normalization to `IbanParser` from `IbanValidator`. This means that the validator is now validating 100% strict according to character pattern rules (character class, case and whitespace, etc.). `ValidationResult.AttemptedValue` will now also have the raw input. Whitespace/casing is still ignored by the `IbanParser`.
- Add Strict-property (default true) to `IbanAttribute` and `FluentIbanValidator` extension. When Strict=true, the input must strictly match the IBAN format rules. When Strict=false, whitespace is ignored and strict character casing enforcement is disabled (meaning, the user can input in lower and uppercase). This mode is a bit more forgiving when dealing with user-input. However it does require after successful validation, that you parse the user input with `IbanParser` to normalize/sanitize the input and to be able to format the IBAN in correct electronic format.
- When normalizing, carriage return (CR) and line feed (LF) are no longer stripped.
- `IllegalCharactersResult` and `InvalidStructureResult` have a property `Position` which indicates the 0-based position at which the first invalid character was encountered.
- ~15-20% Performance improvements to pattern validator.

### IbanNet.DependencyInjection.Autofac

- Fix registration bug where if custom registry was registered, it would still additionally register IbanNet instead of ignoring.

## v5.6.2

- Regenerated Wikipedia registry which corrects IBAN pattern for Senegal (SN).
- Add code gen SWIFT CSV record patchers (so that this no longer requires manual work on each registry update)

## v5.6.1

- [#78](https://github.com/skwasjer/IbanNet/pull/78) Adds branch information to FR

## v5.6.0

- LTS: dropped support for .NET 5.0
- Updated System.ComponentModel.Annotations to v5.0.0
- Updated Autofac to 6.4.0
- Updated FluentValidation to 11.1.0

## v5.5.1

- Upgraded SWIFT registry to May '22 release 92, which adds Djibouti (DJ), fixes the bank pattern for Bahrain (BH).
- Upgraded Wikipedia registry which corrects English name for Sao Tome (ST).

## v5.5.0

- Fixed SWIFT country definitions for which the 'included country codes' contained inferred locales. The list now actually contains the expected country codes.
- LTS: dropped support for .NET 4.5.2 and .NET Standard 1.2/1.3. Added .NET 4.6.2.

## v5.4.0

- Use new `Chunk()` LINQ API for .NET 6 and change own `Partition()` method into polyfill for older framework targets.
- Added `AcceptCountryRule` to restrict validation to a specific set of countries. The rule can be added via the validator options or dependency registration extensions.

## v5.3.2

- Change `master` branch to `main`, requiring update to CI scripts, and update all external page references.

## v5.3.1

- Add package README's

## v5.3.0

- Added .NET 6 target

## v5.2.0

- Added `IbanCountry.NativeName` property which returns the country name in native language, if available.
- Changed the `IbanCountry.DisplayName` property to return the native name, if available; otherwise returns the English name.
- Regenerated `WikipediaRegistryProvider` to include Sudan (SD).
- Implemented `IEquatable<T>`.
- Implemented `IFormattable` with format strings `E`, `P` and `O` for `Electronic`, `Print` and `Obfuscated` respectively.
- Upgraded registry to October '21 release 91 (added Burundi (BI) and updated Sudan (SD))

## v5.1.0

### IbanNet

- Added JSON converter support for `System.Text.Json` to the `Iban` type (>= .NET 5 only)
- Added `iban.IsQrIban()` extension method. A Swiss or Liechtenstein QR-IBAN must have a valid QR-IID, i.e. the bank number must be within the [30000, 31999] range.

## v5.0.0

### IbanNet

- Removed deprecated contracts/code `IStructureValidationFactory`, `IStructureValidation`, `IStructureSection`. Use the `Pattern` abstraction for custom registry providers.
- Removed `Iban.Parse`, `Iban.TryParse`, use the `IbanParser` class.
- Added support for [more countries](./SupportedCountries.md) based on Wikipedia.
- Removed `ValidationMethod`, since performance in strict (= default) is now significantly faster (compared to v4.x, even in loose mode). The maintenance is not worth the little gains anymore.
- Cleaned up API surface/sealed several types.
- Added `Country`, `Bban`, `BankIdentifier` and `BranchIdentifier` properties to the `Iban` value type.
- Removed obsolete facade for `SwiftRegistryProvider`.
- Upgraded registry to June '21 release 90 (added Sudan (SD))

### IbanNet.FluentValidation

- Updated to FluentValidation v10.x, dropping .NET 4.6.1.

### IbanNet.DependencyInjection.*

- `IIbanParser` and `IIbanGenerator` are now registered as singletons.
- Added `IIbanRegistry` as resolveable service (singleton).

## v4.4.3

- Moved `SwiftRegistryProvider` to new namespace, kept facade for backwards compatibility. The facade will be removed in v5.0.

## v4.4.2

- Deprecate loose validation mode. Will be removed in v5.0.

## v4.4.1

- `IIbanRegistry.TryGetValue` now supports case insensitive country codes.

## v4.4.0

- [#21](https://github.com/skwasjer/IbanNet/issues/21) [#31](https://github.com/skwasjer/IbanNet/pull/31) Added `IbanGenerator`.
- LTS: change target frameworks .NET 4.5 and 4.7 to 4.5.2 and 4.7.2 respectively.
- Fix Iraq (IQ) and Finland (FI) patterns (does not affect validation outcome).
-  Introduce pattern abstraction for registry which encapsulates a pattern string and tokenizer. Deprecates `IStructureValidationFactory`, but keeps backwards compatible. Patterns are useful when implementing more/future providers to extend country support.

## v4.3.1

- Upgraded registry to March '21 release 89 (updated Andorra (AD) and El Salvador (SV))

## v4.3.0

- Added .NET 5.0 target framework support ([benchmark](test/IbanNet.Benchmark/BenchmarkResults.md)).
- Update several nullable reference type code contracts.

### IbanNet.FluentValidation

- Updated to FluentValidation v9.x. This also means dropping support for .NETStandard 1.1/1.6 for FluentValidation integration.

### IbanNet.DependencyInjection.Autofac

- (breaking) Updated to Autofac v6.x (one Autofac [interface changed](https://github.com/skwasjer/IbanNet/commit/3a9ec6f43fac943476124065ddbd8cf93ccaede6))

## v4.2.0

- Upgraded registry to September '20 release 88 (added Libya (LY))
- Added `IbanBuilder` and `BbanBuilder` types.

## v4.1.0

### Changes

- IBAN's are now always converted to upper case prior to validation.
- Replaced `Iban.ToString(string)` with `Iban.ToString(IbanFormat)`, and added obfuscated format.

### Fixes

- [#19](https://github.com/skwasjer/IbanNet/issues/19) Parse should only allow non-nullable string.
- [#23](https://github.com/skwasjer/IbanNet/issues/23) AttemptedValue did not match actual value used in validation.
- [#24](https://github.com/skwasjer/IbanNet/issues/24) Structure test will ignore country code casing.

## v4.0.1

- Upgraded registry to May '20 release 87 (changes to ST)

## v4.0.0

### Improvements

- Added `IbanParser` which provides equivalent non-static functionality to `Iban.Parse` and `Iban.TryParse` (which will be obsolete).
- Added .NET Standard 2.1 target
- Enabled and refactored for non-nullable reference types.
- Added abstraction to load registry from different sources.
- Added `ICheckDigitsCalculator` abstraction.
- Exposing `IIbanValidationRule` allowing custom validation rules.
- [Performance improvements](test/IbanNet.Benchmark/BenchmarkResults-v4.x.md)

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

## v3.2.2

- backport: Upgraded registry to May '20 release 87 (changes to ST)

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
- Added [IbanNet.FluentValidation](https://github.com/skwasjer/IbanNet/wiki/IbanNet.FluentValidation)  package.
- Upgraded registry to April '19 release 83.
- Added extra target frameworks `.NET 4.7`, `.NET Standard 1.6` and `.NET Standard 2.0`

## v3.0

- Partial rewrite to support the official [Swift IBAN registry](https://www.swift.com/standards/data-standards/iban).
- Added support for 4 more countries for a total of 76.
- Added details through `CountryInfo`, including IBAN, BBAN, bank and branch structure information, whether a country is a SEPA member and more.
- (breaking) Replaced `IbanValidator.SupportedRegions` with `IbanValidator.SupportedCountries`.
- (breaking) The `IIbanValidator.Validate` method now returns a `ValidationResult` object, instead of an enum value, in order to provide more information of the validation.

## v2.1

- Added [IbanNet.DataAnnotations](https://github.com/skwasjer/IbanNet/wiki/IbanNet.DataAnnotations) for DataAnnotation support.

## v2.0

- (breaking) retarget from .NET Framework 4.5.2 to .NET 4.5.
- Added `TypeConverter` support.

## v1.2

- Expose supported regions via `IbanValidator.SupportedRegions`

## v1.1

- Convert to .NET Standard 1.2

## v1.0

- Initial release with support for 72 countries

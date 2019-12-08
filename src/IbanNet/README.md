# How to use IbanNet

[Package installation](../../README.md)

## Parsing an IBAN

The simplest way to use this library is to use the static `Iban.Parse` method:

```csharp
Iban iban = Iban.Parse("NL91ABNA0417164300");
```

When parsing fails, an `IbanFormatException` is thrown, which contains a property Result which indicates the type of error that occurred.

## Parsing without throwing

To parse an IBAN without throwing an exception use `Iban.TryParse`.

```csharp
Iban iban;
bool success = Iban.TryParse("NL91 ABNA 0417 1643 00", out iban);
```

The only downside is there is no way to retrieve the type of validation error that occurred if parsing fails.

### Formatting an IBAN

To convert an `Iban` type to a string, call the `ToString(string format)` method with either of 2 formats:

- `IbanNet.Iban.Formats.Flat` (default)
- `IbanNet.Iban.Formats.Partitioned`

#### Example

```csharp
Iban iban = Iban.Parse("NL91 ABNA 0417 1643 00");
Debug.WriteLine(iban);                                    // NL91ABNA0417164300
Debug.WriteLine(iban.ToString(Iban.Formats.Flat));        // NL91ABNA0417164300
Debug.WriteLine(iban.ToString(Iban.Formats.Partitioned)); // NL91 ABNA 0417 1643 00
```

By convention, it is recommended to use the flat format when persisting an IBAN in a database, file or otherwise.

### Equals

Iban values can be compared with eachother using `==`, `!=` or `Equals`.

#### Example

```csharp
Iban iban1 = Iban.Parse("NL91 ABNA 0417 1643 00");
Iban iban2 = Iban.Parse("NL91ABNA0417164300");
Iban iban3 = Iban.Parse("BE68 5390 0754 7034");
Debug.WriteLine(iban1 == iban2); // True
Debug.WriteLine(iban1 == iban3); // False
```

### Mocking the static Parse/TryParse methods

If you use the static methods because you would like to use the Iban type, you can mock the underlying validator for testing purposes by setting the static property `Iban.Validator`.

> Take caution in that setting a static singleton is inherently problematic, for instance in unit tests that are run in parallel.

## More control with `IbanValidator`

You can also use the validator directly. The benefit of using the validator is that it implements the `IIbanValidator` interface and can thus be easily mocked. Additionally, the `ValidationResult` provides extra context, like the matched country (if any).

### Example

```csharp
IIbanValidator validator = new IbanValidator();
ValidationResult validationResult = validator.Validate("NL91ABNA041716430");
if (validationResult.IsValid)
{
    // For example:
    if (validationResult.Country.TwoLetterISORegionName != "NL")
    {
        throw new InvalidOperationException("Please provide a Dutch bank account.");
    }
}
```

## Dependency injection

The `IbanValidator` class is thread safe and should be registered as singleton due to some initialization overhead.

### .NET Core example

```csharp
services.AddSingleton<IIbanValidator, IbanValidator>();
```

If you intend to use the static `Parse`/`TryParse` methods as well as the `IIbanValidator`, you can choose to reuse the same validator instance.

```csharp
services.AddSingleton<IIbanValidator>(Iban.Validator);
```

## Requirements

### .NET target frameworks

- .NET Standard 2.1
- .NET Standard 2.0
- .NET Standard 1.6
- .NET Standard 1.2
- .NET 4.7
- .NET 4.5

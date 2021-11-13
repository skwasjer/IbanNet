`Microsoft.Extensions.DependencyInjection` integration for IbanNet; [IbanNet](https://github.com/skwasjer/IbanNet) provides an IBAN validator and parser.

## Available services

This library registers the following services:

- `IIbanValidator`
- `IIbanGenerator`
- `IIbanParser`

## Example

Add IbanNet services:

```csharp
services.AddIbanNet();
```

Or add IbanNet services with configuration:

```csharp
services
    .AddIbanNet(opts => opts
        .UseRegistryProvider(new SwiftRegistryProvider())
        .WithRule<MyCustomRule>()
    );
```

## Other info

- [Changelog](https://github.com/skwasjer/IbanNet/blob/master/CHANGELOG.md)
- [IbanNet supported countries](https://github.com/skwasjer/IbanNet/blob/master/SupportedCountries.md)
- [Fiddle](https://dotnetfiddle.net/JeGa9x)

### Contributions

Please check out the [contribution guidelines](https://github.com/skwasjer/IbanNet/blob/master/CONTRIBUTING.md).

FluentValidation support to validate IBAN user input.

## Example

Create a rule for a property and call the `Iban(IIbanValidator)` extension method to enable the validator.

```csharp
public class InputModel
{
    public string BackAccountNumber { get; set; }
}

public class InputModelValidator : AbstractValidator<InputModel>
{
    public InputModelValidator(IIbanValidator ibanValidator)
    {
        RuleFor(x => x.BankAccountNumber).NotNull().Iban(ibanValidator);
    }
}
```

Prerequisite service registration (.NET Core) of IbanNet.

```csharp
services.AddIbanNet();
services.AddTransient<IValidator<InputModel>, InputModelValidator>()
services.AddFluentValidation();
```

For more information on how to register FluentValidation and custom abstract validators:
https://docs.fluentvalidation.net/en/latest/aspnet.html

## Other info

- [Changelog](https://github.com/skwasjer/IbanNet/blob/main/CHANGELOG.md)
- [IbanNet supported countries](https://github.com/skwasjer/IbanNet/blob/main/SupportedCountries.md)
- [Fiddle](https://dotnetfiddle.net/JeGa9x)

### Contributions

Please check out the [contribution guidelines](https://github.com/skwasjer/IbanNet/blob/main/CONTRIBUTING.md).

# How to use IbanNet.DataAnnotations

Data Annotations support to validate IBAN user input with [IbanNet](../IbanNet/README.md).

[Package installation](../../README.md)

## Introduction

For more detailed info please visit:

- [Basic Introduction to Data Annotation in .Net Framework](https://code.msdn.microsoft.com/Basic-Introduction-to-Data-244734a4)
- [System.ComponentModel.DataAnnotations Namespace](https://msdn.microsoft.com/en-US/library/System.ComponentModel.DataAnnotations.aspx)

## Usage

### By property

```csharp
public class InputModel
{
    [Iban]
    public string BackAccountNumber { get; set; }
}

// MVC
public class MyMvcController : Controller
{
    [HttpPost]
    public ActionResult Save(InputModel model)
    {
        if (ModelState.IsValid)
        {
            // ..
        }
    }
}

// Web API
public class MyWebApiController : ApiController
{
    [HttpPost]
    public IHttpActionResult Save(InputModel model)
    {
        if (ModelState.IsValid)
        {
            // ..
        }
    }
}

```

### By parameter

```csharp
public class MyController : Controller
{
    [HttpPost]
    public ActionResult Save([Iban] string bankAccountNumber)
    {
        if (ModelState.IsValid)
        {
            // ..
        }
    }
}
```

## Dependency injection

You can use your favorite DI provider to provide an [`IIbanValidator`](../IbanNet/IIbanValidator.cs) to the validation attribute, as long as `IServiceProvider.GetService(Type)` is implemented.
If no instance of `IIbanValidator` is resolved from the DI container, the static `Iban.IbanValidator` property is used instead.

### .NET Core example

```csharp
services.AddSingleton<IIbanValidator, IbanValidator>();
```

## FAQ

### How about directly using the Iban type?

Because the [`Iban`](../../README.md) type itself has `TypeConverter` support, it can also be directly used in an input model, negating the need for this library.

#### Example

```csharp
public class InputModel
{
   public Iban BackAccountNumber { get; set; }
}
```

> See [IbanTypeConverter](../IbanNet/TypeConverters/IbanTypeConverter.cs). The converter will be called during model binding allowing proper deserialization.

However in this situation - when a validation error occurs - instead of gracefully populating the `ModelStateDictionary`, an exception is thrown during model binding. Whether or not that is acceptable depends on the project/team.

### Mapping to Iban type

The `string` value can be mapped using an Object Mapper to an `Iban` type using [`Iban.Parse`](../../README.md#parse) or manually by providing an extra property:

#### Example

```csharp
public class InputModel
{
    [Iban]
    public string BackAccountNumber { get; set; }

    [JsonIgnore]
    internal Iban InternalBackAccountNumber => BackAccountNumber != null ? Iban.Parse(BackAccountNumber) : null;
}
```

Because validation has already occurred, the `Parse` method should always succeed.

## Requirements

### .NET target frameworks

- .NET Standard 2.1
- .NET Standard 2.0
- .NET Standard 1.6
- .NET Standard 1.3
- .NET 4.7
- .NET 4.5

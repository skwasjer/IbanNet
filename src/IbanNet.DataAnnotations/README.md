[![Build status](https://ci.appveyor.com/api/projects/status/469oo89bngrkgh2l?svg=true)](https://ci.appveyor.com/project/skwasjer/ibannet)
[![NuGet](https://img.shields.io/nuget/v/IbanNet.DataAnnotations.svg)](https://www.nuget.org/packages/IbanNet.DataAnnotations/)
[![Tests](https://img.shields.io/appveyor/tests/skwasjer/IbanNet.svg)](https://ci.appveyor.com/project/skwasjer/ibannet/build/tests)

# IbanNet.DataAnnotations

Data Annotations to validate IBAN user input.

## Introduction

To validate user input in ASP.NET MVC and Web API, Microsoft provides .NET Data Annotations. 
While there are other alternatives to validate user input, this library specifically provides Data Annotation support which allows **IbanNet** to easily integrate in existing MVC and Web API projects for both .NET Framework and .NET Core.

For more detailed info please visit:
- [Basic Introduction to Data Annotation in .Net Framework](https://code.msdn.microsoft.com/Basic-Introduction-to-Data-244734a4)
- [System.ComponentModel.DataAnnotations Namespace](https://msdn.microsoft.com/en-US/library/System.ComponentModel.DataAnnotations.aspx)

## Usage

#### By field or property ####

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

#### By parameter ####

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

## FAQ
### Why would I need this package?

While there are different alternatives to validate user input, a lot of projects rely on ASP.NET MVC and/or Web API which already use Data Annotations.

### How about directly using the Iban type?

Because the [`Iban`](../../README.md) type itself has `TypeConverter` support, it can also be directly used in an input model, negating the need for this library.

#### Example ####

```csharp
public class InputModel
{	
	public Iban BackAccountNumber { get; set; }
}
```

> See [IbanTypeConverter](../IbanNet/IbanTypeConverter.cs). The converter will be called during model binding allowing proper deserialization.

However in this situation - when a validation error occurs - instead of gracefully populating the `ModelStateDictionary`, an exception is thrown during model binding. Whether or not that is acceptable depends on the project/team.

### Mapping to Iban type

The `string` value can be mapped using an Object Mapper to an `Iban` type using [`Iban.Parse`](../../README.md#parse) or manually by providing an extra property:

#### Example ####

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

## Installation

Install **IbanNet.DataAnnotations** via [NuGet](https://www.nuget.org/packages/IbanNet.DataAnnotations/).

#### Visual Studio / .NET Framework
```powershell
Install-Package IbanNet.DataAnnotations
```

#### .NET CLI
```
dotnet add package IbanNet.DataAnnotations
```

### Future enhancements
- Poll: add client side validation support via AJAX?
- Localization

#### Supported .NET targets
- .NET 4.5 or greater
- .NET Standard 1.3 or greater

#### Requirements
- Visual Studio 2017
- .NET Core 2.0 SDK

#### Contributions
PR's are welcome. Please rebase before submitting, provide test coverage, and ensure the AppVeyor build passes. I will not consider PR's otherwise.

#### Contributors
- skwas (author/maintainer)

## Related libraries
- [IbanNet](../../README.md)
[![Build status](https://ci.appveyor.com/api/projects/status/469oo89bngrkgh2l?svg=true)](https://ci.appveyor.com/project/skwasjer/ibannet)
[![NuGet](https://img.shields.io/nuget/v/IbanNet.svg)](https://www.nuget.org/packages/IbanNet/)
[![Tests](https://img.shields.io/appveyor/tests/skwasjer/IbanNet.svg)](https://ci.appveyor.com/project/skwasjer/ibannet/build/tests)

# IbanNet.DataAnnotations

Data annotations to validate IBAN user input.

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
		// ..
	}
}

// Web API
public class MyWebApiController : ApiController
{
	[HttpPost]
	public IHttpActionResult Save(InputModel model)
	{
		// ..
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
		// ..
	}
}
```

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

#### Supported .NET targets
- .NET 4.5 or greater
- .NET Standard 1.2 or greater

#### Requirements
- Visual Studio 2017
- .NET Core 2.0 SDK

#### Contributions
PR's are welcome. Please rebase before submitting, provide test coverage, and ensure the AppVeyor build passes. I will not consider PR's otherwise.

#### Contributors
- skwas (author/maintainer)

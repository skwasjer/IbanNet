`IbanNet.DataAnnotations` provides an attribute allowing validation of IBAN user input with ASP.NET (Core) and Microsoft Data Annotations.

## Example

```csharp
public class InputModel
{
    [Iban]
    [Required]
    public string BackAccountNumber { get; set; }
}

[ApiController]
public class MyController : ControllerBase
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
```

## Other info

- [Changelog](https://github.com/skwasjer/IbanNet/blob/main/CHANGELOG.md)
- [IbanNet supported countries](https://github.com/skwasjer/IbanNet/blob/main/SupportedCountries.md)
- [Fiddle](https://dotnetfiddle.net/JeGa9x)

### Contributions

Please check out the [contribution guidelines](https://github.com/skwasjer/IbanNet/blob/main/CONTRIBUTING.md).

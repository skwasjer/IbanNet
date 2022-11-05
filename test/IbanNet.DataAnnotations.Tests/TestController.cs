#if ASPNET_INTEGRATION_TESTS
using Microsoft.AspNetCore.Mvc;

namespace IbanNet.DataAnnotations;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpPost("save")]
    public IActionResult Save(InputModel iban)
    {
        return Ok(iban.BankAccountNumber);
    }

    [HttpPost("save-strict")]
    public IActionResult Save(StrictInputModel iban)
    {
        return Ok(iban.BankAccountNumber);
    }
}
#endif

#if ASPNET_INTEGRATION_TESTS
using Microsoft.AspNetCore.Mvc;

namespace IbanNet.DataAnnotations
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpPost("save")]
        public IActionResult Save(InputModel iban)
        {
#if NETCOREAPP2_0 || NETCOREAPP2_1
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
#endif

            return Ok(iban.BankAccountNumber);
        }
    }
}
#endif

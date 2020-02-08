using ExampleWebApplication.Models;
using IbanNet;
using Microsoft.AspNetCore.Mvc;

namespace ExampleWebApplication.Controllers
{
	/// <summary>
	/// Web API example, showing usage of IbanNet.
	/// </summary>
	[Route("api/[controller]")]
	public class WebApiController : ControllerBase
	{
		private readonly IIbanParser _parser;

		public WebApiController(IIbanParser parser)
		{
			_parser = parser;
		}

		[HttpPost]
		public IActionResult Save([FromBody] InputModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Iban iban = _parser.Parse(model.BankAccountNumber);
			// Do something with model...
			model.BankAccountNumber = iban.ToString(Iban.Formats.Partitioned);

			return Ok(model);
		}
	}
}

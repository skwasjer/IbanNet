using System.Web.Http;
using ExampleWebApplication.Models;
using IbanNet;
using IbanNet.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ExampleWebApplication.Controllers
{
	/// <summary>
	/// Web API 2 example, showing usage of <see cref="IbanAttribute"/>.
	/// </summary>
	public class WebApiController : ApiController
	{
		[HttpPost]
		public IActionResult Save(InputModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Iban iban = Iban.Parse(model.BankAccountNumber);
			// Do something with model...
			model.BankAccountNumber = iban.ToString(Iban.Formats.Partitioned);

			return Json(model);
		}
	}
}
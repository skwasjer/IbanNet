using ExampleWebApplication.Models;
using IbanNet;
using Microsoft.AspNetCore.Mvc;

namespace ExampleWebApplication.Controllers
{
	/// <summary>
	/// MVC example, showing usage of IbanNet validation.
	/// </summary>
	public class MvcController : Controller
	{
		private readonly IIbanParser _parser;

		public MvcController(IIbanParser parser)
		{
			_parser = parser;
		}

		// GET
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Save(InputModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("Index");
			}

			Iban iban = _parser.Parse(model.BankAccountNumber);
			// Do something with model...
			ModelState.Clear();
			model.BankAccountNumber = iban.ToString(Iban.Formats.Partitioned);

			return View("Index", model);
		}
	}
}

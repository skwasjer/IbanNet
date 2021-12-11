using ExampleWebApplication.Models;
using IbanNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleWebApplication.Pages
{
	/// <summary>
	/// Razor Page example, showing usage of IbanNet.
	/// </summary>
	public class RazorPageModel : PageModel
	{
		private readonly IIbanParser _parser;

		public RazorPageModel(IIbanParser parser)
		{
			_parser = parser;
		}

		[BindProperty]
		public InputModel Model { get; set; }

		public IActionResult OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			Iban iban = _parser.Parse(Model.BankAccountNumber);
			// Do something with model...
			Model.BankAccountNumber = iban.ToString(IbanFormat.Print);

			return Page();
		}
	}
}

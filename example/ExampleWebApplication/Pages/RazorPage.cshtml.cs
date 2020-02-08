using System.ComponentModel.DataAnnotations;
using ExampleWebApplication.Models;
using IbanNet;
using IbanNet.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleWebApplication.Pages
{
	/// <summary>
	/// Razor Page example, showing usage of <see cref="IbanAttribute"/>.
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
			Model.BankAccountNumber = iban.ToString(Iban.Formats.Partitioned);

			return Page();
		}
	}
}

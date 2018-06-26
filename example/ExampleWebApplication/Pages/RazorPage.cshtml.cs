using System.ComponentModel.DataAnnotations;
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
		[BindProperty]
		[Required]
		[Iban]
		public string BankAccountNumber { get; set; }

		public IActionResult OnPostAsync(RazorPageModel model)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			Iban iban = Iban.Parse(model.BankAccountNumber);
            // Do something with model...
            BankAccountNumber = iban.ToString(Iban.Formats.Partitioned);

            return Page();
		}
	}
}
using IbanNet.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace ExampleWebApplication.Models
{
    public class InputModel
    {
		[Required]
		[Iban]
		public string BankAccountNumber { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace IbanNet.DataAnnotations
{
	public class InputModel
	{
		[Required]
		[Iban]
		public string BankAccountNumber { get; set; }
	}
}

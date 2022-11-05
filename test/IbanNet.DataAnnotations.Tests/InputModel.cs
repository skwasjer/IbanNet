using System.ComponentModel.DataAnnotations;

namespace IbanNet.DataAnnotations;

public class InputModel
{
    [Required]
    [Iban(Strict = false)]
    public string BankAccountNumber { get; set; }
}
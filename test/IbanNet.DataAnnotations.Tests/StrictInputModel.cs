using System.ComponentModel.DataAnnotations;

namespace IbanNet.DataAnnotations;

public class StrictInputModel
{
    [Required]
    [Iban(Strict = true)]
    public string BankAccountNumber { get; set; } = default!;
}

using IbanNet;

namespace AutoMapperExample.Domain;

public class Payment
{
    public Payment(Iban bankAccountNumber, decimal amount, string currency)
    {
        BankAccountNumber = bankAccountNumber;
        Amount = amount;
        Currency = currency;
    }

    public Iban BankAccountNumber { get; }
    public decimal Amount { get; }
    public string Currency { get; }
}

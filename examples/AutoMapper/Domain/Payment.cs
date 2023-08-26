using IbanNet;

namespace AutoMapperExample.Domain;

public record Payment(Iban BankAccountNumber, decimal Amount, string Currency);

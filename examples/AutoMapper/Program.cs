using AutoMapper;
using AutoMapperExample.Domain;
using AutoMapperExample.Dtos;
using AutoMapperExample.Mappings;
using IbanNet;
using IbanNet.Registry;

var generator = new IbanGenerator();
IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new PaymentProfile(new IbanParser(IbanRegistry.Default))))
    .CreateMapper();

do
{
    Console.WriteLine("1. Map from a valid IBAN string");
    Console.WriteLine("2. Map from an invalid IBAN string");

    ConsoleKey key = Console.ReadKey().Key;
    Console.WriteLine();

    string input = generator.Generate("NL").ToString();
    // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
    switch (key)
    {
        case ConsoleKey.D1:
            break;
        case ConsoleKey.D2:
            // Removing 1 char from end, makes IBAN invalid.
            input = input[..^1];
            break;
        default:
            return;
    }

    var dto = new PaymentDto(input, 15, "EUR");

    try
    {
        Payment payment = mapper.Map<Payment>(dto);
        Console.WriteLine($"Payment mapped: {payment.Amount} {payment.Currency} from account {payment.BankAccountNumber:E}");
    }
    catch (AutoMapperMappingException ex)
        when (ex.InnerException is IbanFormatException)
    {
        Console.WriteLine("Unable to map payment, because the input IBAN string is invalid.");
    }
} while (true);

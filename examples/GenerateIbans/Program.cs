using IbanNet;
using IbanNet.Registry;

var generator = new IbanGenerator();

do
{
    Console.Write("Type a 2 letter country code to generate an IBAN for and press ENTER: ");
    string? input = Console.ReadLine();
    if (input is null)
    {
        return;
    }

    Iban iban;
    try
    {
        iban = generator.Generate(input);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("Invalid country code.");
        Console.WriteLine();
        continue;
    }

    Console.WriteLine($"  Random IBAN for country {iban.Country.TwoLetterISORegionName}: {iban:P}");
    Console.WriteLine();
} while (true);

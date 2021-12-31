using IbanNet;
using IbanNet.Validation.Rules;

var validatorOptions = new IbanValidatorOptions
{
    Rules =
    {
        new AcceptCountryRule(new[] { "NL", "DE" })
    }
};

var validator = new IbanValidator(validatorOptions);

TryValidate("NL91ABNA0417164300");
TryValidate("BE68539007547034"); // Not allowed
TryValidate("FR1420041010050500013M02606"); // Not allowed
TryValidate("DE89370400440532013000");

void TryValidate(string iban)
{
    ValidationResult result = validator.Validate(iban);
    Console.WriteLine($"{result.Country!.TwoLetterISORegionName}: {result.Error?.ErrorMessage ?? "Success"}");
}

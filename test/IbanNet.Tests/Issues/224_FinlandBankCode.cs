using IbanNet.Registry;

namespace IbanNet.Issues;

public sealed class _224_FinlandBankCode
{
    [Theory]
    [InlineData("FI4017453000182859", "174530")]
    [InlineData("FI2840554920113105", "405549")]
    public void When_extracting_bank_code_it_should_return_expected(string ibanStr, string expectedBankCode)
    {
        var ibanParser = new IbanParser(IbanRegistry.Default);

        // Act
        bool isValid = ibanParser.TryParse(ibanStr, out Iban? iban);

        // Assert
        isValid.Should().BeTrue();
        iban!.BankIdentifier.Should().Be(expectedBankCode);
    }
}

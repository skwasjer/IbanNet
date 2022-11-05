using IbanNet.Registry;

namespace IbanNet.Extensions;

public class SwissIbanExtensionsTests
{
    [Fact]
    public void Given_that_iban_is_null_when_getting_IsQrIban_it_should_throw()
    {
        Iban? iban = null;

        // Act
        Func<bool> act = () => iban!.IsQrIban();

        // Assert
        act.Should()
            .ThrowExactly<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be(nameof(iban));
    }

    [Theory]
    [InlineData("CH58 3000 0000 0000 0000 0")]
    [InlineData("CH07 3100 0000 0000 0000 0")]
    [InlineData("CH54 3100 0111 1111 1111 1")]
    [InlineData("CH45 3199 9000 0000 0000 0")]
    [InlineData("LI71 3000 0000 0000 0000 0")]
    [InlineData("LI20 3100 0000 0000 0000 0")]
    [InlineData("LI67 3100 0111 1111 1111 1")]
    [InlineData("LI58 3199 9000 0000 0000 0")]
    public void Given_iban_is_a_valid_qr_iban_when_getting_IsQrIban_it_should_be_true(string ibanString)
    {
        Iban iban = new IbanParser(IbanRegistry.Default).Parse(ibanString);

        iban.IsQrIban().Should().BeTrue();
    }

    [Theory]
    [InlineData("CH50 2999 9000 0000 0000 0")]
    [InlineData("LI63 2999 9000 0000 0000 0")]
    [InlineData("CH53 3200 0000 0000 0000 0")]
    [InlineData("LI66 3200 0000 0000 0000 0")]
    [InlineData("DE61 0003 0000 1111 1111 11")]
    public void Given_iban_is_valid_but_not_a_valid_qr_iban_when_getting_IsQrIban_it_should_be_false(string ibanString)
    {
        Iban iban = new IbanParser(IbanRegistry.Default).Parse(ibanString);

        iban.IsQrIban().Should().BeFalse();
    }
}

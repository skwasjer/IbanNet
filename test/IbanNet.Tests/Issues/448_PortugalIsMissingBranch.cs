using IbanNet.Registry;

namespace IbanNet.Issues;

/// <summary>
/// Issue #448
/// </summary>
public class _448_PortugalIsMissingBranch
{
    [Fact]
    public void When_extracting_portugal_branch_code_it_should_return_expected()
    {
        var ibanParser = new IbanParser(IbanRegistry.Default);

        // Act
        bool isValid = ibanParser.TryParse("PT50002700000001234567833", out Iban? iban);

        // Assert
        isValid.Should().BeTrue();
        iban!.BankIdentifier.Should().Be("0027");
        iban.BranchIdentifier.Should()
            .NotBeNull()
            .And.Be("0000");
    }
}

using IbanNet.Registry;

namespace IbanNet.Issues;

/// <summary>
/// Issue #77
/// </summary>
public class _77_FranceIsMissingBranch
{
    [Fact]
    public void When_extracting_france_branch_code_it_should_return_expected()
    {
        var ibanParser = new IbanParser(IbanRegistry.Default);

        // Act
        bool isValid = ibanParser.TryParse("FR7630001007941234567890185", out Iban? iban);

        // Assert
        isValid.Should().BeTrue();
        iban!.BranchIdentifier.Should()
            .NotBeNull()
            .And.Be("00794");
    }
}

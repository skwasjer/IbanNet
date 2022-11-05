using IbanNet.Registry;
using IbanNet.Registry.Patterns;

namespace IbanNet.Builders;

public class BankAccountBuilderTests
{
    [Theory]
    [InlineData(typeof(BbanBuilder))]
    [InlineData(typeof(IbanBuilder))]
    public void Given_null_country_when_adding_it_should_throw(Type builderType)
    {
        IBankAccountBuilder builder = CreateBuilder(builderType);
        IbanCountry? country = null;

        // Act
        Func<IBankAccountBuilder> act = () => builder.WithCountry(country!);

        // Assert
        act.Should()
            .ThrowExactly<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be(nameof(country));
    }

    [Theory]
    [InlineData(typeof(BbanBuilder))]
    [InlineData(typeof(IbanBuilder))]
    public void Given_country_is_not_set_when_building_it_should_throw(Type builderType)
    {
        string exSource = builderType.Name.Substring(0, 4).ToUpperInvariant();

        IBankAccountBuilder builder = CreateBuilder(builderType);

        // Act
        Action act = () => builder.Build();

        // Assert
        act.Should()
            .ThrowExactly<BankAccountBuilderException>()
            .WithMessage($"The {exSource} cannot be built.")
            .WithInnerException<InvalidOperationException>()
            .WithMessage("The country is required.");
    }

    [Theory]
    [InlineData(typeof(BbanBuilder))]
    [InlineData(typeof(IbanBuilder))]
    public void Given_country_does_not_have_bban_pattern_when_building_it_should_throw(Type builderType)
    {
        string exSource = builderType.Name.Substring(0, 4).ToUpperInvariant();

        IBankAccountBuilder builder = CreateBuilder(builderType)
            .WithCountry(new IbanCountry("XX"));

        // Act
        Action act = () => builder.Build();

        // Assert
        act.Should()
            .ThrowExactly<BankAccountBuilderException>()
            .WithMessage($"The {exSource} cannot be built.")
            .WithInnerException<InvalidOperationException>()
            .WithMessage("The country 'XX' does not define a BBAN pattern.");
    }

    [Theory]
    [InlineData(typeof(BbanBuilder), false)]
    [InlineData(typeof(IbanBuilder), true)]
    public void Given_country_does_not_have_iban_pattern_when_building_it_should_throw(Type builderType, bool shouldThrow)
    {
        string exSource = builderType.Name.Substring(0, 4).ToUpperInvariant();

        IBankAccountBuilder builder = CreateBuilder(builderType)
            .WithCountry(new IbanCountry("XX")
            {
                Bban = new BbanStructure(
                    new FakePattern(new[] { new PatternToken(AsciiCategory.Digit, 10) })
                )
            });

        // Act
        Action act = () => builder.Build();

        // Assert
        if (shouldThrow)
        {
            act.Should()
                .ThrowExactly<BankAccountBuilderException>()
                .WithMessage($"The {exSource} cannot be built.")
                .WithInnerException<InvalidOperationException>()
                .WithMessage("The country 'XX' does not define a IBAN pattern.");
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [Theory]
    [InlineData(typeof(BbanBuilder), "NL", "00000000000000")]
    [InlineData(typeof(BbanBuilder), "GB", "000000000000000000")]
    [InlineData(typeof(IbanBuilder), "NL", "NL2200000000000000")]
    [InlineData(typeof(IbanBuilder), "GB", "GB18000000000000000000")]
    public void Given_only_country_when_building_it_should_not_throw(Type builderType, string countryCode, string expected)
    {
        IBankAccountBuilder builder = CreateBuilder(builderType)
            .WithCountry(countryCode, IbanRegistry.Default);

        // Act
        Func<string> act = () => builder.Build();

        // Assert
        act.Should()
            .NotThrow()
            .Which.Should()
            .Be(expected);
    }

    [Theory]
    [InlineData(typeof(BbanBuilder), "NL", "123", "00000000000123")]
    [InlineData(typeof(BbanBuilder), "GB", "123", "000000000000000123")]
    [InlineData(typeof(IbanBuilder), "NL", "789", "NL5900000000000789")]
    [InlineData(typeof(IbanBuilder), "GB", "789", "GB55000000000000000789")]
    public void Given_bankAccountNumber_when_building_it_should_return_value(Type builderType, string countryCode, string bankAccountNumber, string expected)
    {
        IBankAccountBuilder builder = CreateBuilder(builderType)
            .WithCountry(countryCode, IbanRegistry.Default)
            .WithBankAccountNumber(bankAccountNumber);

        // Act
        string actual = builder.Build();

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BuilderMethodTestCases))]
    public void Given_value_is_too_short_and_padding_is_disabled_when_building_it_should_throw(
        Type builderType,
        Action<IBankAccountBuilder, string, bool> @delegate
    )
    {
        string exSource = builderType.Name.Substring(0, 4).ToUpperInvariant();

        IBankAccountBuilder builder = CreateBuilder(builderType)
            .WithCountry("GB", IbanRegistry.Default);
        @delegate(builder, "1", false);

        // Act
        Action act = () => builder.Build();

        // Assert
        act.Should()
            .ThrowExactly<BankAccountBuilderException>()
            .WithMessage($"The {exSource} cannot be built.")
            .WithInnerException<InvalidOperationException>()
            .WithMessage("The value '1' does not have the correct length of *.");
    }

    [Theory]
    [MemberData(nameof(TooShortWithPaddingTestCases))]
    public void Given_value_is_too_short_and_padding_is_enabled_when_building_it_should_pad_with_zeroes(
        Type builderType,
        Action<IBankAccountBuilder, string, bool> @delegate,
        string countryCode,
        string value,
        string expected
    )
    {
        IBankAccountBuilder builder = CreateBuilder(builderType)
            .WithCountry(countryCode, IbanRegistry.Default);
        @delegate(builder, value, true);

        // Act
        Func<string> act = () => builder.Build();

        // Assert
        act.Should()
            .NotThrow()
            .Which.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BuilderMethodTestCases))]
    public void Given_value_is_too_long_when_building_it_should_throw(
        Type builderType,
        Action<IBankAccountBuilder, string, bool> @delegate
    )
    {
        string exSource = builderType.Name.Substring(0, 4).ToUpperInvariant();

        IBankAccountBuilder builder = CreateBuilder(builderType)
            .WithCountry("GB", IbanRegistry.Default);
        @delegate(builder, new string('0', 100), false);

        // Act
        Action act = () => builder.Build();

        // Assert
        act.Should()
            .ThrowExactly<BankAccountBuilderException>()
            .WithMessage($"The {exSource} cannot be built.")
            .WithInnerException<InvalidOperationException>()
            .WithMessage("The value '*' does not have the correct length of *.");
    }

    [Theory]
    [InlineData(typeof(BbanBuilder))]
    [InlineData(typeof(IbanBuilder))]
    public void Given_country_does_not_support_branchIdentifier_when_building_it_should_throw(Type builderType)
    {
        string exSource = builderType.Name.Substring(0, 4).ToUpperInvariant();

        IBankAccountBuilder builder = CreateBuilder(builderType)
            .WithCountry("NL", IbanRegistry.Default)
            .WithBranchIdentifier("123");

        // Act
        Action act = () => builder.Build();

        // Assert
        act.Should()
            .ThrowExactly<BankAccountBuilderException>()
            .WithMessage($"The {exSource} cannot be built.")
            .WithInnerException<InvalidOperationException>()
            .WithMessage("A value for 'Branch' is not supported for country code NL.");
    }

    private static IBankAccountBuilder CreateBuilder(Type builderType)
    {
        return (IBankAccountBuilder)Activator.CreateInstance(builderType)!;
    }

    public static IEnumerable<object[]> BuilderMethodTestCases()
    {
        Type bbanBuilder = typeof(BbanBuilder);
        Type ibanBuilder = typeof(IbanBuilder);
        Action<IBankAccountBuilder, string, bool> bankAccount = (b, value, pad) => b.WithBankAccountNumber(value, pad);
        Action<IBankAccountBuilder, string, bool> branch = (b, value, pad) => b.WithBranchIdentifier(value, pad);
        Action<IBankAccountBuilder, string, bool> bank = (b, value, pad) => b.WithBankIdentifier(value, pad);

        yield return new object[] { bbanBuilder, bankAccount };
        yield return new object[] { bbanBuilder, branch };
        yield return new object[] { bbanBuilder, bank };

        yield return new object[] { ibanBuilder, bankAccount };
        yield return new object[] { ibanBuilder, branch };
        yield return new object[] { ibanBuilder, bank };
    }

    public static IEnumerable<object[]> TooShortWithPaddingTestCases()
    {
        Type bbanBuilder = typeof(BbanBuilder);
        Type ibanBuilder = typeof(IbanBuilder);
        Action<IBankAccountBuilder, string, bool> bankAccount = (b, value, pad) => b.WithBankAccountNumber(value, pad);
        Action<IBankAccountBuilder, string, bool> branch = (b, value, pad) => b.WithBranchIdentifier(value, pad);
        Action<IBankAccountBuilder, string, bool> bank = (b, value, pad) => b.WithBankIdentifier(value, pad);

        yield return new object[] { bbanBuilder, bankAccount, "GB", "1", "000000000000000001" };
        yield return new object[] { bbanBuilder, branch, "GB", "1", "000000000100000000" };
        yield return new object[] { bbanBuilder, bank, "GB", "1", "000100000000000000" };

        yield return new object[] { ibanBuilder, bankAccount, "GB", "1", "GB88000000000000000001" };
        yield return new object[] { ibanBuilder, branch, "GB", "1", "GB62000000000100000000" };
        yield return new object[] { ibanBuilder, bank, "GB", "1", "GB42000100000000000000" };
    }
}

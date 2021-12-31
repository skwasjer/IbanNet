using IbanNet.Registry;

namespace IbanNet.Builders
{
    /// <summary>
    /// A builder to build bank account numbers.
    /// </summary>
    public interface IBankAccountBuilder : IFluentInterface
    {
        /// <summary>
        /// Adds the specified <paramref name="country" /> to the builder.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <returns>The builder to continue chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="country" /> is null.</exception>
        IBankAccountBuilder WithCountry(IbanCountry country);

        /// <summary>
        /// Adds the specified <paramref name="bankIdentifier" /> to the builder.
        /// </summary>
        /// <param name="bankIdentifier">The bank identifier.</param>
        /// <param name="enablePadding"><see langword="true" /> to enable automatic left padding with '0' if a value is too short, <see langword="false" /> to disable padding and throw exceptions if a value is too short.</param>
        /// <returns>The builder to continue chaining.</returns>
        IBankAccountBuilder WithBankIdentifier(string bankIdentifier, bool enablePadding = true);

        /// <summary>
        /// Adds the specified <paramref name="branchIdentifier" /> to the builder.
        /// </summary>
        /// <param name="branchIdentifier">The branch identifier.</param>
        /// <param name="enablePadding"><see langword="true" /> to enable automatic left padding with '0' if a value is too short, <see langword="false" /> to disable padding and throw exceptions if a value is too short.</param>
        /// <returns>The builder to continue chaining.</returns>
        IBankAccountBuilder WithBranchIdentifier(string branchIdentifier, bool enablePadding = true);

        /// <summary>
        /// Adds the specified <paramref name="bankAccountNumber" /> to the builder.
        /// </summary>
        /// <param name="bankAccountNumber">The bank account number.</param>
        /// <param name="enablePadding"><see langword="true" /> to enable automatic left padding with '0' if a value is too short, <see langword="false" /> to disable padding and throw exceptions if a value is too short.</param>
        /// <returns>The builder to continue chaining.</returns>
        IBankAccountBuilder WithBankAccountNumber(string bankAccountNumber, bool enablePadding = true);

        /// <summary>
        /// Creates the bank account number.
        /// </summary>
        /// <returns>The bank account number.</returns>
        /// <exception cref="BankAccountBuilderException">Thrown when bank account number cannot be built.</exception>
        string Build();
    }
}

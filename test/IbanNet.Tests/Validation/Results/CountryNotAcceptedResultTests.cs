using System.Globalization;
using IbanNet.Registry;

namespace IbanNet.Validation.Results
{
    public class CountryNotAcceptedResultTests
    {
        [Fact]
        public void Given_that_country_is_null_when_creating_instance_it_should_throw()
        {
            IbanCountry country = null;

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            Func<CountryNotAcceptedResult> act = () => new CountryNotAcceptedResult(country);

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(country));
        }

        [Fact]
        public void When_creating_instance_it_should_have_expected_message()
        {
            var country = new IbanCountry("NL") { DisplayName = "The display name" };
            var sut = new CountryNotAcceptedResult(country);
            sut.ErrorMessage.Should()
                .Be(string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.CountryNotAcceptedResult_Bank_account_numbers_from_country_0_are_not_accepted,
                    country.DisplayName)
                );
        }
    }
}

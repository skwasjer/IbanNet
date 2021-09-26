#if ASPNET_INTEGRATION_TESTS
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace IbanNet.DataAnnotations
{
    public class AspNetIntegrationTest : IClassFixture<AspNetWebHostFixture>
    {
        private readonly AspNetWebHostFixture _fixture;

        public AspNetIntegrationTest(AspNetWebHostFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Given_valid_iban_when_posting_with_attribute_validation_it_should_validate()
        {
            const string validIban = "NL91 ABNA 0417 1643 00";
            using HttpClient client = _fixture.TestServer.CreateClient();

            // Act
            HttpResponseMessage response = await client.SendAsync(CreateSaveRequest(validIban));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsStringAsync()).Should().Be($"\"{validIban}\"");
        }

        [Fact]
        public async Task Given_invalid_iban_when_posting_with_attribute_validation_it_should_validate()
        {
            const string invalidIban = "invalid-iban";
            using HttpClient client = _fixture.TestServer.CreateClient();

            // Act
            HttpResponseMessage response = await client.SendAsync(CreateSaveRequest(invalidIban));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            string responseContent = await response.Content.ReadAsStringAsync();
            _fixture.MapToErrors(responseContent)
                .Should()
                .ContainKey("BankAccountNumber")
#if FlUENT_ASSERTIONS_5
                .WhichValue.Should()
#else
                .WhoseValue.Should()
#endif
                .Contain("The field 'BankAccountNumber' is not a valid IBAN.");
        }

        private static HttpRequestMessage CreateSaveRequest(string iban)
        {
            return new(HttpMethod.Post, "test/save")
            {
                Headers =
                {
                    Accept =
                    {
                        new MediaTypeWithQualityHeaderValue("application/json")
                    }
                },
                Content = new StringContent($"{{\"BankAccountNumber\":\"{iban}\"}}", Encoding.UTF8, "application/json")
            };
        }
    }
}
#endif

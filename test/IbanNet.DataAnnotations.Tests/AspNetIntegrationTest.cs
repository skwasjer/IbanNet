#if ASPNET_INTEGRATION_TESTS
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#else
using Newtonsoft.Json;
#endif

namespace IbanNet.DataAnnotations;

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
        HttpResponseMessage response = await client.SendAsync(CreateSaveRequest(validIban, false));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.Content.ReadAsStringAsync()).Should().Be($"\"{validIban}\"");
    }

    [Fact]
    public async Task Given_invalid_iban_when_posting_with_attribute_validation_it_should_not_validate()
    {
        const string invalidIban = "invalid-iban";
        using HttpClient client = _fixture.TestServer.CreateClient();

        // Act
        HttpResponseMessage response = await client.SendAsync(CreateSaveRequest(invalidIban, false));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
#if NET5_0_OR_GREATER
        ValidationProblemDetails? problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
#else
        ValidationProblemDetails? problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(await response.Content.ReadAsStringAsync());
#endif
        problemDetails.Should().NotBeNull();
        problemDetails!.Errors
            .Should()
            .ContainKey("BankAccountNumber")
            .WhoseValue.Should()
            .Contain("The field 'BankAccountNumber' is not a valid IBAN.");
    }

    private static HttpRequestMessage CreateSaveRequest(string iban, bool strict)
    {
        return new HttpRequestMessage(HttpMethod.Post, "test/save" + (strict ? "-strict" : ""))
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
#endif

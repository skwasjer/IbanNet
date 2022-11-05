#if ASPNET_INTEGRATION_TESTS
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace IbanNet.DataAnnotations;

public abstract class WebHostFixture : IDisposable
{
    public TestServer TestServer { get; private set; } = default!;

    public void Start()
    {
        IWebHostBuilder webHostBuilder = new WebHostBuilder();
        Configure(webHostBuilder);
        TestServer = new TestServer(webHostBuilder);
    }

    protected virtual void Configure(IWebHostBuilder webHostBuilder)
    {
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        TestServer?.Dispose();
    }
}
#endif

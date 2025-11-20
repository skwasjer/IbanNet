#if ASPNET_INTEGRATION_TESTS
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace IbanNet.DataAnnotations;

public abstract class WebHostFixture : IDisposable, IAsyncDisposable
{
    private IHost? _host;
    private bool _isDisposed;

    public TestServer TestServer { get; private set; } = null!;

    public async Task StartAsync()
    {
        _host = new HostBuilder()
            .ConfigureWebHost(webHostBuilder =>
            {
                webHostBuilder
                    .UseTestServer()
                    .UseContentRoot(Directory.GetCurrentDirectory());
                Configure(webHostBuilder);
            })
            .Build();
        await _host.StartAsync();

        TestServer = _host.GetTestServer();
    }

    protected virtual void Configure(IWebHostBuilder webHostBuilder)
    {
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                TestServer?.Dispose();
                TestServer = null!;
                _host?.Dispose();
                _host = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual async ValueTask DisposeAsync()
    {
        if (!_isDisposed)
        {
            if (_host is IAsyncDisposable hostAsyncDisposable)
            {
                await hostAsyncDisposable.DisposeAsync();
            }

            _host = null;
        }

        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
#endif

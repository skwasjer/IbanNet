#if ASPNET_INTEGRATION_TESTS
using IbanNet.DependencyInjection.ServiceProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace IbanNet.DataAnnotations
{
    public class TestStartup
    {
#pragma warning disable CA1822 // Mark members as static
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822 // Mark members as static
        {
            services
                .AddIbanNet()
                .AddMvc()
                .AddControllersAsServices();
        }

#pragma warning disable CA1822 // Mark members as static
        public void Configure(IApplicationBuilder app)
#pragma warning restore CA1822 // Mark members as static
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class AspNetWebHostFixture : WebHostFixture, IAsyncLifetime
    {
        protected override void Configure(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseStartup<TestStartup>();
            base.Configure(webHostBuilder);
        }

        public Task InitializeAsync()
        {
            Start();
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}
#endif

#if ASPNET_INTEGRATION_TESTS
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace IbanNet.DataAnnotations
{
    public class TestStartup
    {
#pragma warning disable CA1822 // Mark members as static
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822 // Mark members as static
        {
            services
                .AddSingleton<IIbanValidator, IbanValidator>()
                .AddMvc()
#if NETCOREAPP2_1
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
#endif
                .AddControllersAsServices();
        }

#pragma warning disable CA1822 // Mark members as static
        public void Configure(IApplicationBuilder app)
#pragma warning restore CA1822 // Mark members as static
        {
#if ENDPOINT_ROUTING
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
#else
			app.UseMvc();
#endif
        }
    }

    public class AspNetWebHostFixture : WebHostFixture, IAsyncLifetime
    {
        protected override void Configure(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseStartup<TestStartup>();
            base.Configure(webHostBuilder);
        }

        public override IDictionary<string, string[]> MapToErrors(string jsonContent)
        {
#if PROBLEM_DETAILS
            return JsonConvert.DeserializeObject<ValidationProblemDetails>(jsonContent).Errors;
#else
			return JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonContent);
#endif
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

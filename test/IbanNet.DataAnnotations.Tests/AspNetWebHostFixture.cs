#if ASPNET_INTEGRATION_TESTS
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace IbanNet.DataAnnotations
{
	public class TestStartup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddSingleton<IIbanValidator, IbanValidator>()
				.AddMvc()
#if NETCOREAPP2_1
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
#endif
#if NETCOREAPP2_2
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
#endif
				.AddControllersAsServices();
		}

		public virtual void Configure(IApplicationBuilder app)
		{
#if NETCOREAPP3_1
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
#if NETCOREAPP2_2 || NETCOREAPP3_1
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

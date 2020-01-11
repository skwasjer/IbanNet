#if NETCOREAPP3_1
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace IbanNet.DataAnnotations
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		[HttpPost("save")]
		public IActionResult Save(InputModel iban)
		{
			return Ok(iban.BankAccountNumber);
		}
	}

	public class AspNet30WebHostFixture : WebHostFixture
	{
		private class TestStartup
		{
			public void ConfigureServices(IServiceCollection services)
			{
				services
					.AddSingleton<IIbanValidator, IbanValidator>()
					.AddControllers()
					.AddControllersAsServices();
			}

			public void Configure(IApplicationBuilder app)
			{
				app.UseRouting();
				app.UseEndpoints(endpoints =>
				{
					endpoints.MapControllers();
				});
			}
		}

		protected override void Configure(IWebHostBuilder webHostBuilder)
		{
			webHostBuilder.UseStartup<TestStartup>();
			base.Configure(webHostBuilder);
		}

		public override IDictionary<string, string[]> MapToErrors(string jsonContent)
		{
			return JsonConvert.DeserializeObject<ValidationProblemDetails>(jsonContent).Errors;
		}
	}
}
#endif

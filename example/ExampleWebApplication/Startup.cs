#if DEBUG_FLUENTVALIDATION
using FluentValidation;
using FluentValidation.AspNetCore;
#endif
using IbanNet.DependencyInjection;
using IbanNet.DependencyInjection.ServiceProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExampleWebApplication
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Register IbanNet.
			services.AddIbanNet(opts => opts.UseStrictValidation());
			services
				.AddRazorPages()
#if DEBUG_FLUENTVALIDATION
				.AddFluentValidation(fv => fv
					.RegisterValidatorsFromAssemblyContaining<Startup>()
					// Disable DataAnnotations/IValidateObject (not required, but to isolate this as only validation framework)
					.RunDefaultMvcValidationAfterFluentValidationExecutes = false
				)
#endif
				;

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				// For Mvc.
				endpoints.MapDefaultControllerRoute();
				// For RazorPages.
				endpoints.MapRazorPages();
				// For API controllers using attribute routing.
				endpoints.MapControllers();
			});
		}
	}
}

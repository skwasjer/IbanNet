#if DEBUG_FLUENTVALIDATION
using FluentValidation.AspNetCore;
#endif
using IbanNet.DependencyInjection.ServiceProvider;

namespace AspNetCoreExample;

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
        services.AddIbanNet();
        services
            .AddRazorPages()
#if DEBUG_FLUENTVALIDATION
				.AddFluentValidation(fv => fv
					.RegisterValidatorsFromAssemblyContaining<Startup>()
                    .DisableDataAnnotationsValidation = true
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

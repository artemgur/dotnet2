using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<ICalculator, Calculator>();
		}
		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();
			app.UseCalculator("expression");
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/calculate", async context =>
				{
					//Everything useful is done by middleware
				});
			});
		}
	}
}
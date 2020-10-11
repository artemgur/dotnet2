using Microsoft.AspNetCore.Builder;

namespace Calculator
{
	public static class MiddlewareExtensions
	{
		public static void UseCalculator(this IApplicationBuilder app, string pattern)
		{
			app.UseMiddleware<CalculatorMiddleware>(pattern);
		}
	}
}
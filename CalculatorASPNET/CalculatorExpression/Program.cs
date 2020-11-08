using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorExpression
{
	public static class Program
	{
		public static async Task Main()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddScoped<ICalculator, ProxyCalculator>();
			Console.WriteLine(await Calculator.CalculateAsync(Console.ReadLine(), serviceCollection));
		}
	}
}
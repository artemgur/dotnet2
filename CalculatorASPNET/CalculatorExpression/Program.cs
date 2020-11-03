using System;
using System.Threading.Tasks;

namespace CalculatorExpression
{
	public static class Program
	{
		public static async Task Main()
		{
			Console.WriteLine(await Calculator.CalculateAsync(Console.ReadLine()));
		}
	}
}
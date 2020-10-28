using System;
using System.Threading.Tasks;

namespace CalculatorExpression
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine(await Calculator.CalculateAsync(Console.ReadLine()));
		}
	}
}
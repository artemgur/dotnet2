using System;
using System.Net;
using System.Threading.Tasks;

namespace CalculatorExpression
{
	public class ProxyCalculator: ICalculator
	{
		private static string url = "https://localhost:5001/calculate?expression=";

		public async Task<double> Calculate(double a, char operation, double b)
		{
			var address = url + a + ConvertOperator(operation) + b;
			var request = WebRequest.Create(address);
			var response = await request.GetResponseAsync();
			return double.Parse(response.Headers["calculator_result"]);
		}
		
		private static string ConvertOperator(char operation) =>
			operation switch
			{
				'+' => "%2B",
				'-' => "-",
				'*' => "%2A",
				'/' => "%2F",
				_ => throw new ArgumentException("Expression tree contains not supported operations")
			};
	}
}
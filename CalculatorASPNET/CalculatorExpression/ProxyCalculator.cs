using System.Net;
using System.Threading.Tasks;

namespace CalculatorExpression
{
	public class ProxyCalculator: ICalculator
	{
		private static string url = "https://localhost:5001/calculate?expression=";

		public async Task<string> Calculate(double a, string operation, double b)
		{
			var address = url + a + operation + b;
			var request = WebRequest.Create(address);
			var response = await request.GetResponseAsync();
			return response.Headers["calculator_result"];
		}
	}
}
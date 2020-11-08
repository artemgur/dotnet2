using System.Threading.Tasks;

namespace CalculatorExpression
{
	public interface ICalculator
	{
		public Task<string> Calculate(double a, char operation, double b);
	}
}
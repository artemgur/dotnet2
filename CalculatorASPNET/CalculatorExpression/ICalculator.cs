using System.Threading.Tasks;

namespace CalculatorExpression
{
	public interface ICalculator
	{
		public Task<double> Calculate(double a, char operation, double b);
	}
}
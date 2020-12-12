using System.Threading.Tasks;
using CalculatorExpression;

namespace CalculatorExpressionTests
{
	public class DirectCalculator:ICalculator
	{
		public async Task<double> Calculate(double a, char operation, double b) =>
			double.Parse(Calculator.Calculator.Evaluate(a, b, operation));
	}
}
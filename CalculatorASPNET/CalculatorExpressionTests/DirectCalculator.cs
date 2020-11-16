using System.Threading.Tasks;
using CalculatorExpression;

namespace CalculatorExpressionTests
{
	public class DirectCalculator:ICalculator
	{
		public async Task<string> Calculate(double a, char operation, double b) =>
			Calculator.Calculator.Evaluate(a, b, operation);
	}
}
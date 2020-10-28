using System.Threading.Tasks;

namespace CalculatorExpression
{
	public static class Calculator
	{
		public static async Task<double> CalculateAsync(string expression)
		{
			var visitor = new CalculatorExpressionVisitor();
			var e = ExpressionTreeBuilder.Build(expression);
			visitor.Visit(e);
			return await visitor.MainTask;
		}
	}
}
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorExpression
{
	public static class Calculator
	{
		public static async Task<double> CalculateAsync(string expression, ServiceCollection collection)
		{
			var visitor = new CalculatorExpressionVisitor(collection);
			var e = ExpressionTreeBuilder.Build(expression);
			return await visitor.VisitTree(e);
		}
	}
}
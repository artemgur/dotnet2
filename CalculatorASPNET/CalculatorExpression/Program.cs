using System;
using System.Linq.Expressions;

namespace CalculatorExpression
{
	class Program
	{
		static void Main(string[] args)
		{
			var expression = ExpressionTreeBuilder.Build("1.5/5");
			var lambda = Expression.Lambda<Func<double>>(expression);
			var compiled = lambda.Compile();
			Console.WriteLine(compiled());
		}
	}
}
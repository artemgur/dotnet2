using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorExpression
{
	public class CalculatorExpressionVisitor
	{
		private readonly ServiceProvider serviceProvider;
		private readonly ICalculator calculator;

		public CalculatorExpressionVisitor(ServiceCollection serviceCollection)
		{
			serviceProvider = serviceCollection.BuildServiceProvider();
			calculator = serviceProvider.GetService<ICalculator>();
		}

		public Task<double> VisitTree(Expression expression) => Visit((dynamic)expression);
		
		public async Task<double> Visit(BinaryExpression expression)
		{
			var l = (dynamic) expression.Left;
			var r = (dynamic) expression.Right;
			return await calculator.Calculate(await Visit(l), GetOperator(expression), await Visit(r));
		}

		public async Task<double> Visit(ConstantExpression expression) => (double)expression.Value;
		
		private static char GetOperator(BinaryExpression expression) =>
		expression.NodeType switch
		{
			ExpressionType.Add => '+',
			ExpressionType.Subtract => '-',
			ExpressionType.Multiply => '*',
			ExpressionType.Divide => '/',
			_ => throw new ArgumentException("Expression tree contains not supported operations")
		};
	}
}
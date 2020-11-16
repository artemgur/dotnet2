using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorExpression
{
	public class CalculatorExpressionVisitor : ExpressionVisitor
	{
		private readonly ConcurrentDictionary<Expression, Task<double>> Tasks = new ConcurrentDictionary<Expression, Task<double>>();
		private readonly ServiceProvider serviceProvider;
		private readonly ICalculator calculator;

		public CalculatorExpressionVisitor(ServiceCollection serviceCollection)
		{
			serviceProvider = serviceCollection.BuildServiceProvider();
			calculator = serviceProvider.GetService<ICalculator>();
		}

		public Task<double> VisitTree(Expression expression) => GetTask(expression);

		private Task<double> GetTask(Expression expression)
		{
			return Task.Run(async () =>
			{
				if (expression is ConstantExpression e1)
					return (double)e1.Value;
				var e = (BinaryExpression) expression;
				Visit(e.Left);
				Visit(e.Right);
				var results = await Task.WhenAll(Tasks[e.Left], Tasks[e.Right]);
				Tasks.Remove(e.Left, out var a);
				Tasks.Remove(e.Right, out var b);
				// var address = url + results[0] + GetOperator(e) + results[1];
				// var request = WebRequest.Create(address);
				// var response = await request.GetResponseAsync();
				// var result = double.Parse(response.Headers["calculator_result"], CultureInfo.InvariantCulture);
				var @operator = GetOperator(e);
				var result = double.Parse(await calculator.Calculate(results[0], @operator, results[1]));
				Console.WriteLine(results[0] + @operator + results[1] + "=" + result);
				return result;
			});
		}

		public override Expression Visit(Expression expression)
		{
			var task = GetTask(expression);
			Tasks[expression] = task;
			return expression;
		}

		// private static string GetOperator(BinaryExpression expression) =>
		// 	expression.NodeType switch
		// 	{
		// 		ExpressionType.Add => "%2B",
		// 		ExpressionType.Subtract => "-",
		// 		ExpressionType.Multiply => "%2A",
		// 		ExpressionType.Divide => "%2F",
		// 		_ => throw new ArgumentException("Expression tree contains not supported operations")
		// 	};
		
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
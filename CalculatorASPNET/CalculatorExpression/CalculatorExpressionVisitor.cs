using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace CalculatorExpression
{
	public class CalculatorExpressionVisitor : ExpressionVisitor
	{
		private static string url = "https://localhost:5001/calculate?expression=";
		public readonly ConcurrentDictionary<Expression, Task<double>> Tasks = new ConcurrentDictionary<Expression, Task<double>>();
		public Task<double> MainTask;

		public override Expression Visit(Expression expression)
		{
			var task = Task.Run(async () =>
			{
				if (expression is ConstantExpression e1)
					return (double)e1.Value;
				var e = (BinaryExpression) expression;
				Visit(e.Left);
				Visit(e.Right);
				var results = await Task.WhenAll(Tasks[e.Left], Tasks[e.Right]);
				Tasks.Remove(e.Left, out var a);
				Tasks.Remove(e.Right, out var b);
				var address = url + results[0] + GetOperator(e) + results[1];
				//Console.WriteLine(address);
				var request = WebRequest.Create(address);
				var response = await request.GetResponseAsync();
				return double.Parse(response.Headers["calculator_result"], CultureInfo.InvariantCulture);
			});
			if (MainTask == null)
				MainTask = task;
			Tasks[expression] = task;
			return expression;
		}

		private static string GetOperator(BinaryExpression expression) =>
			expression.NodeType switch
			{
				ExpressionType.Add => "%2B",
				ExpressionType.Subtract => "-",
				ExpressionType.Multiply => "%2A",
				ExpressionType.Divide => "%2F",
				_ => throw new ArgumentException("Expression tree contains not supported operations")
			};
	}
}
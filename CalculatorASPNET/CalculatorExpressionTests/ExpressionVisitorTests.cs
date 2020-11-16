using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CalculatorExpression;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CalculatorExpressionTests
{
	public class ExpressionVisitorTests
	{
		[Theory]
		[InlineData("2+2*2")]
		[InlineData("(5+3)*(5-2)")]
		[InlineData("(4*(3+5)-4-8/2-(6-4)/2)*((2+4)*4-(8-5)/3)-5")]
		[InlineData("(((9-6/2)*2-4)/2-6-1)/(2+24/(2+4))")]
		[InlineData("(5+6)*7-9/(5-4*(5-2))")]
		[InlineData("((5+3)*3-(8-2)/2)/2")]
		[InlineData("145*695/25-42")]
		[InlineData("0.2/(5*(3+6+1)-1.3)+3.52-6.2*(5-6)")]
		[InlineData("2*2*(3+2*(57.5-23.5/7))")]
		[InlineData("(1+2+3)*4/5*(6-7+8*9-10)")]
		public async Task IsExpressionVisitorResultEqualToCompiledExpression(string expression)
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddScoped<ICalculator, DirectCalculator>();
			var compiledResult = Expression.Lambda<Func<double>>(ExpressionTreeBuilder.Build(expression)).Compile()();
			var visitorResult = await CalculatorExpression.Calculator.CalculateAsync(expression, serviceCollection);
			Assert.Equal(compiledResult, visitorResult);
		}
	}
}
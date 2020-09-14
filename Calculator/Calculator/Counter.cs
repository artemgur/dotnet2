using System;

namespace Calculator
{
	public static class Counter
	{
		public static double Count(double a, double b, char @operator) => @operator switch
		{
			'+' => a + b,
			'-' => a - b,
			'*' => a * b,
			'/' => a / b,
			'^' => Math.Pow(a, b),
			_=> throw new NotImplementedException("The operation doesn't exist")
		};
	}
}
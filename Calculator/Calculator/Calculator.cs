using System;

namespace Calculator
{
	public static class Calculator
	{
		public static double Calculate(double a, double b, char @operator)
		{
			switch (@operator)
			{
				case '+':
					return a + b;
				case '-':
					return a - b;
				case '*':
					return a * b;
				case '/':
					return Divide(a, b);
				case '^':
					return Math.Pow(a, b);
				default:
					throw new NotImplementedException("The operation doesn't exist");
			}
		}

		private static double Divide(double a, double b)
		{
			if (b == 0)
				throw new DivideByZeroException();
			return a / b;
		}
	}
}
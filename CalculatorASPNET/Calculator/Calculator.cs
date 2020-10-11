using System;
using System.Globalization;

namespace Calculator
{
	public static class Calculator
	{
		private static readonly char[] Operators = {'+', '-', '*', '/', '^'};
		
		public const string ErrorString = "error";
		
		public static double Evaluate(double a, double b, char @operator)
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
					throw new NotImplementedException("The operation doesn't exist"); //Shouldn't be reached
			}
		}

		public static string Calculate(string s)
		{
			var operatorPos = s.IndexOfAny(Operators);
			if (operatorPos == -1)
			{
				if (double.TryParse(s, out var x))
					return s;
				//throw new ArgumentException("String doesn't contain valid operators and is not a number");
				return ErrorString;
			}
			if (double.TryParse(s.Substring(0, operatorPos), out var a) &&
			    double.TryParse(s.Substring(operatorPos + 1), out var b))
				return Evaluate(a, b, s[operatorPos]).ToString(CultureInfo.InvariantCulture);
			//throw new ArgumentException("One or both of the numbers in expression are invalid");
			return ErrorString;
		}

		private static double Divide(double a, double b)
		{
			if (b == 0)
				throw new DivideByZeroException();
			return a / b;
		}
	}
}
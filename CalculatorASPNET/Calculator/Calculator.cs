using System;
using System.Globalization;

namespace Calculator
{
	public static class Calculator
	{
		private static readonly char[] Operators = {'+', '-', '*', '/', '^'};
		
		public const string ErrorInvalidNumberString = "One or both numbers are invalid";
		public const string ErrorNoOperatorString = "String doesn't contain valid operators. Valid operators include +, -, *, /, ^";
		public const string ErrorDivideByZeroString = "You tried to divide by 0";
		
		public static string Evaluate(double a, double b, char @operator)
		{
			switch (@operator)
			{
				case '+':
					return (a + b).ToString(CultureInfo.InvariantCulture);
				case '-':
					return (a - b).ToString(CultureInfo.InvariantCulture);
				case '*':
					return (a * b).ToString(CultureInfo.InvariantCulture);
				case '/':
					return Divide(a, b);
				case '^':
					return Math.Pow(a, b).ToString(CultureInfo.InvariantCulture);
				default:
					return ErrorNoOperatorString; //Shouldn't be reached
			}
		}

		public static string Calculate(string s)
		{
			var operatorPos = s.IndexOfAny(Operators);
			if (operatorPos == -1)
				return ErrorNoOperatorString;
			if (double.TryParse(s.Substring(0, operatorPos), out var a) &&
			    double.TryParse(s.Substring(operatorPos + 1), out var b))
				return Evaluate(a, b, s[operatorPos]).ToString(CultureInfo.InvariantCulture);
			return ErrorInvalidNumberString;
		}

		private static string Divide(double a, double b)
		{
			if (b == 0)
				return ErrorDivideByZeroString;
			return (a / b).ToString(CultureInfo.InvariantCulture);
		}
	}
}
using System;
using System.Globalization;
using System.Linq;

namespace Calculator
{
	public class Calculator:ICalculator
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

		public string Calculate(string s)
		{
			s = new string(s.Where(c => c != '(' && c != ')').ToArray()); //To support expressions like 2+(-9)
			var operatorPos = s.IndexOfAny(Operators, 1);//if s[0] is operator, either expression is invalid, or it is '-' and first number is negative
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
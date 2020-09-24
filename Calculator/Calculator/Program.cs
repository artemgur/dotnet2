using System;

namespace Calculator
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var a = ReadDouble();
			var @operator = char.Parse(Console.ReadLine());
			var b = ReadDouble();
			Console.WriteLine(Calculator.Calculate(a, b, @operator));
		}

		private static double ReadDouble()
		{
			var result = 0.0;
			if (!double.TryParse(Console.ReadLine(), out result))
				throw new ArgumentException("Input is not a number");
			return result;
		}
	}
}
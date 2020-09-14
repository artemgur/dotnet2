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

		private static double ReadDouble() => double.Parse(Console.ReadLine());
	}
}
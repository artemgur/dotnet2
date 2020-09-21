using System;
using NUnit.Framework;

namespace CalculatorTests
{
	public static class CalculatorTests
	{
		[TestCase(0, 0)]
		[TestCase(2, 2)]
		[TestCase(5, -7)]
		public static void TestSum(double a, double b) => Assert.AreEqual(a + b, Calculator.Calculator.Calculate(a, b, '+'));

		[TestCase(0, 0)]
		[TestCase(2, 2)]
		[TestCase(5, -7)]
		public static void TestSubtraction(double a, double b) => Assert.AreEqual(a - b, Calculator.Calculator.Calculate(a, b, '-'));

		[TestCase(0, 0)]
		[TestCase(2, 2)]
		[TestCase(5, -7)]
		public static void TestMultiplication(double a, double b) => Assert.AreEqual(a * b, Calculator.Calculator.Calculate(a, b, '*'));

		[TestCase(0, 0)]
		[TestCase(2, 2)]
		[TestCase(5, -7)]
		public static void TestDivision(double a, double b) => Assert.AreEqual(a / b, Calculator.Calculator.Calculate(a, b, '/'));

		[TestCase(0, 5)]
		[TestCase(2, 2)]
		[TestCase(5, -7)]
		public static void TestPower(double a, double b) => Assert.AreEqual(Math.Pow(a, b), Calculator.Calculator.Calculate(a, b, '^'));
	}
}
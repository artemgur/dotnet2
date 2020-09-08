using System;
using NUnit.Framework;

namespace Calculator
{
	public static class CalculatorTests
	{
		[TestCase(0, 0)]
		[TestCase(2, 2)]
		[TestCase(5, -7)]
		public static void AdditionTest(double a, double b) => Assert.AreEqual(a + b, Calculator.Calculate(a, b, '+'));
		
		[TestCase(0, 0)]
		[TestCase(2, 2)]
		[TestCase(5, -7)]
		public static void SubtractionTest(double a, double b) => Assert.AreEqual(a - b, Calculator.Calculate(a, b, '-'));
		
		[TestCase(0, 0)]
		[TestCase(2, 2)]
		[TestCase(5, -7)]
		public static void MultiplicationTest(double a, double b) => Assert.AreEqual(a * b, Calculator.Calculate(a, b, '*'));
		
		[TestCase(0, 0)]
		[TestCase(2, 2)]
		[TestCase(5, -7)]
		public static void DivisionTest(double a, double b) => Assert.AreEqual(a / b, Calculator.Calculate(a, b, '/'));
		
		[TestCase(0, 5)]
		[TestCase(2, 2)]
		[TestCase(5, -7)]
		public static void PowerTest(double a, double b) => Assert.AreEqual(Math.Pow(a, b), Calculator.Calculate(a, b, '^'));
	}
}
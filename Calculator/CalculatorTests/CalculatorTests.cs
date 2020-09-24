using System;
using NUnit.Framework;

namespace CalculatorTests
{
	public static class CalculatorTests
	{
		private static double delta = 1e-5;
		
		[TestCase(0, 0, TestName = "Calculate_0Plus0_0Returned")]
		[TestCase(2, 2, TestName = "Calculate_2Plus2_4Returned")]
		[TestCase(5.5, -7, TestName = "Calculate_5.5Plus-7_-1.5Returned")]
		public static void TestSum(double a, double b) => Assert.AreEqual(a + b, Calculator.Calculator.Calculate(a, b, '+'), delta);

		[TestCase(0, 0, TestName = "Calculate_0Minus0_0Returned")]
		[TestCase(2, 0.5, TestName = "Calculate_2Minus0.5_1.5Returned")]
		[TestCase(5, -7, TestName = "Calculate_5Minus-7_12Returned")]
		public static void TestSubtraction(double a, double b) => Assert.AreEqual(a - b, Calculator.Calculator.Calculate(a, b, '-'), delta);

		[TestCase(0, 0, TestName = "Calculate_0MultiplyOn0_0Returned")]
		[TestCase(2, 1.5, TestName = "Calculate_2MultiplyOn1.5_3Returned")]
		[TestCase(5, -7, TestName = "Calculate_5MultiplyOn-7_-35Returned")]
		public static void TestMultiplication(double a, double b) => Assert.AreEqual(a * b, Calculator.Calculator.Calculate(a, b, '*'), delta);

		[TestCase(0, 5, TestName = "Calculate_0DivideBy5_0Returned")]
		[TestCase(2, 2, TestName = "Calculate_2DivideBy2_4Returned")]
		[TestCase(5, 0.5, TestName = "Calculate_5DivideBy0.5_10Returned")]
		public static void TestDivision(double a, double b) => Assert.AreEqual(a / b, Calculator.Calculator.Calculate(a, b, '/'), delta);

		[Test]
		public static void Calculate_8DivideBy0_DivideByZeroExceptionThrown()
		{
			Assert.Throws(typeof(DivideByZeroException), () => Calculator.Calculator.Calculate(8, 0, '/'));
		}
		
		[TestCase(0, 5, TestName = "Calculate_0InPower5_0Returned")]
		[TestCase(4, 0.5, TestName = "Calculate_4InPower0.5_2Returned")]
		[TestCase(5, 0, TestName = "Calculate_5InPower0_1Returned")]
		public static void TestPower(double a, double b) => Assert.AreEqual(Math.Pow(a, b), Calculator.Calculator.Calculate(a, b, '^'), delta);

		[Test]
		public static void Calculate_InvalidOperation_NotImplementedExceptionThrown()
		{
			Assert.Throws(typeof(NotImplementedException), () => Calculator.Calculator.Calculate(8, 9, '$'));
		}
	}
}
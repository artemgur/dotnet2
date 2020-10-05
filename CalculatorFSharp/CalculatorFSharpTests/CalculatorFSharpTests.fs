module CalculatorFSharpTests

open NUnit.Framework
open CalculatorFSharp

[<TestCase(0, 0, TestName = "Calculate_0Plus0_0Returned")>]
[<TestCase(2, 2, TestName = "Calculate_2Plus2_4Returned")>]
[<TestCase(5.5, -7, TestName = "Calculate_5.5Plus-7_-1.5Returned")>]
let TestSum a b =
    Assert.AreEqual(Some(a + b), calculate "+" (Some a) (Some b))
    
[<TestCase(0, 0, TestName = "Calculate_0Minus0_0Returned")>]
[<TestCase(2, 0.5, TestName = "Calculate_2Minus0.5_1.5Returned")>]
[<TestCase(5, -7, TestName = "Calculate_5Minus-7_12Returned")>]
let TestSubtract a b =
    Assert.AreEqual(Some(a - b), calculate "-" (Some a) (Some b))
    
[<TestCase(0, 0, TestName = "Calculate_0MultiplyOn0_0Returned")>]
[<TestCase(2, 1.5, TestName = "Calculate_2MultiplyOn1.5_3Returned")>]
[<TestCase(5, -7, TestName = "Calculate_5MultiplyOn-7_-35Returned")>]
let TestMultiply a b =
    Assert.AreEqual(Some(a * b), calculate "*" (Some a) (Some b))
    
[<TestCase(0, 5, TestName = "Calculate_0DivideBy5_0Returned")>]
[<TestCase(2, 2, TestName = "Calculate_2DivideBy2_4Returned")>]
[<TestCase(5, 0.5, TestName = "Calculate_5DivideBy0.5_10Returned")>]
let TestDivide a b =
    Assert.AreEqual(Some(a / b), calculate "/" (Some a) (Some b))

[<Test>]
let TestDivideBy0NoneReturned () =
    Assert.AreEqual(None, calculate "/" (Some 8.) (Some 0.))

[<Test>]
let Calculate_InvalidOperationNoneReturned () =
    Assert.AreEqual(None, calculate "&" (Some 8.) (Some 7.))
    
let NoneArgument op a b =
    Assert.AreEqual(None, calculate op a b)
    
[<Test>]
let Calculate_FirstArgumentIsNone_NoneReturned () =
    NoneArgument "+" None (Some 3.)
    
[<Test>]
let Calculate_SecondArgumentIsNone_NoneReturned () =
    NoneArgument "/" (Some 3.) None
    
[<Test>]
let Calculate_BothArgumentsAreNone_NoneReturned () =
    NoneArgument "/" None None
    
[<Test>]
let Parse_NumberString_NumberReturned () =
    Assert.AreEqual(Some(5.), parse "5")
    
[<Test>]
let Parse_NonNumberString_None () =
    Assert.AreEqual(None, parse "some_text")
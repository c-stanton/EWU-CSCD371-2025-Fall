using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculate;
using System;
using System.Collections.Generic;

namespace CalculateTests.Tests;

[TestClass]
public sealed class CalulateTests
{
    [TestMethod]
    public void Properties_SetAtConstruction()
    {
        //Arrange
        string? output1 = null;
        string? output2 = null;

        string nameInput = "Messi";

        var program = new Program
        {
            WriteLine = r =>
            {
                if (output1 == null)
                {
                    output1 = r;
                }
                else
                {
                    output2 = r;
                }
            },
            ReadLine = () => nameInput
        };

        //Act
        program.WriteLine("Enter name:");
        string? input = program.ReadLine();
        program.WriteLine($"Hey There, {input}");

        //Assert
        Assert.AreEqual("Enter name:", output1);
        Assert.AreEqual("Hey There, Messi", output2);
        Assert.AreEqual("Messi", input);
    }

    [TestMethod]
    public void Add_ValidInput_ReturnsSum()
    {
        // Arrange
        double a = 5;
        double b = 3;

        // Act
        double result = Calculator<double>.Add(a, b);

        // Assert
        Assert.AreEqual<double>(8, result);
    }

    [TestMethod]
    public void Subtract_ValidInput_ReturnsDifference()
    {
        // Arrange
        double a = 5;
        double b = 3;

        // Act
        double result = Calculator<double>.Subtract(a, b);

        // Assert
        Assert.AreEqual<double>(2, result);
    }

    [TestMethod]
    public void Multiply_ValidInput_ReturnsProduct()
    {
        // Arrange
        double a = 67;
        double b = 7;

        // Act
        double result = Calculator<double>.Multiply(a, b);

        // Assert
        Assert.AreEqual<double>(469, result);
    }

    [TestMethod]
    public void Divide_ValidInput_ReturnsQuotient()
    {
        // Arrange
        double a = 6;
        double b = 3;

        // Act
        double result = Calculator<double>.Divide(a, b);

        // Assert
        Assert.AreEqual<double>(2, result);
    }

    [TestMethod]
    public void Divide_DivideByZero_ReturnsNaN()
    {
        // Arrange
        double a = 6;
        double b = 0;

        // Act
        double result = Calculator<double>.Divide(a, b);

        // Assert
        Assert.IsTrue(double.IsNaN(result));
    }

    [TestMethod]

    public void TryCalculate_Expression_ReturnsCorrectResult()
    {
        // Arrange
        string expression = "10 + 5";
        double expected = 15;

        // Act
        bool input = Calculator<double>.TryCalculate(expression, out double result);

        // Assert
        Assert.IsTrue(input);
        Assert.AreEqual<double>(expected, result);
    }

    [TestMethod]
    public void TryCalculate_InvalidExpression_ReturnsFalse()
    {
        //Arrange
        string expression = "10 * 5";

        //Act
        bool input = Calculator<double>.TryCalculate(expression, out double result);

        //Assert
        Assert.IsTrue(input);
        Assert.AreEqual<double>(50, result);
    }

    [TestMethod]
    public void TryCalculate_ExpressionMissingSpace_ReturnsFalse()
    {
        // Arrange
        string expression = "67+67";

        // Act
        bool input = Calculator<double>.TryCalculate(expression, out double result);

        // Assert
        Assert.IsFalse(input);
    }

    [TestMethod]
    public void TryCalculate_DivideByZero_ReturnsFalse()
    {
        // Arrange
        string expression = "314762 / 0";

        // Act
        bool input = Calculator<double>.TryCalculate(expression, out double result);

        // Assert
        Assert.IsFalse(input);
    }

    [TestMethod]
    public void MathematicalOperations_ContainsAllOperations()
    {
        // Arrange
        var operations = Calculator<double>.MathematicalOperations;

        // Act & Assert
        Assert.IsTrue(operations.ContainsKey('+'));
        Assert.IsTrue(operations.ContainsKey('-'));
        Assert.IsTrue(operations.ContainsKey('*'));
        Assert.IsTrue(operations.ContainsKey('/'));

        Assert.AreEqual<double>(13, operations['+'](6, 7));
        Assert.AreEqual<double>(18, operations['-'](21, 3));
        Assert.AreEqual<double>(1400, operations['*'](100, 14));
        Assert.AreEqual<double>(2, operations['/'](6, 3));
    }

    [TestMethod]
    public void Run_Completes_Successfully()
    {
        //Arrange
        String[] inputs =
        {
            "10 + 5",
            "20 - 4",
            "3 * 7",
            "16 / 2",
            ""
        };

        int Index = 0;
        var outputs = new List<string>();

        var program = new Program
        {
            ReadLine = () => inputs[Index++],
            WriteLine = s => outputs.Add(s)
        };

        //Act
        program.Run();

        //Assert
        Assert.AreEqual<string>("Result: 15", outputs[2]);
        Assert.AreEqual<string>("Result: 16", outputs[4]);
        Assert.AreEqual<string>("Result: 21", outputs[6]);
        Assert.AreEqual<string>("Result: 8", outputs[8]);
    }

    [TestMethod]
    public void Run_InvalidExpression_ReturnsPrompt()
    {
        //Arrange
        String[] inputs =
        {
            "10 / 0",
            "5 ++ 67",
            ""
        };

        int Index = 0;
        var outputs = new List<string>();

        var program = new Program
        {
            ReadLine = () => inputs[Index++],
            WriteLine = s => outputs.Add(s)
        };

        //Act
        program.Run();

        //Assert
        Assert.AreEqual<string>("Invalid expression. Please try again.", outputs[2]);
        Assert.AreEqual<string>("Invalid expression. Please try again.", outputs[4]);
        Assert.AreEqual<string>("Thanks 4 playin!", outputs[7]);
    }

    [TestMethod]
    public void DecimalCalculator_PreciseMath_ReturnsCorrectDecimal()
    {
        // Assert
        decimal a = 10.00m;
        decimal b = 3.00m;
        decimal expected = 3.3333333333333333333333333333m;

        // Act
        decimal result = Calculator<decimal>.Divide(a, b);

        // Assert
        Assert.AreEqual<decimal>(expected, result);
    }

    [TestMethod]
    public void IntCalculator_Division_TruncatesResult()
    {
        // Arrange
        int a = 8;
        int b = 3;
        int expected = 2;

        // Act
        int result = Calculator<int>.Divide(a,b);

        // Assert
        Assert.AreEqual<int>(expected, result);
    }
}

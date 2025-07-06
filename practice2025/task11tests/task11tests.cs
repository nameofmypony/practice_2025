namespace task11tests;
using task11;
using Xunit;

public class CalculatorTests
{
    private const string CalculatorCode = @"
public class Calculator
{
    public int Sum(int a, int b) => a + b;
    public int Dif(int a, int b) => a - b;
    public int Mul(int a, int b) => a * b;
    public int Div(int a, int b) => a / b;
}";

    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(-1, 5, 4)]
    public void Sum_ReturnsCorrectResult(int a, int b, int expected)
    {
        ICalculator calculator = Calculator.GenerateCalculator(CalculatorCode);
        Assert.Equal(expected, calculator.Sum(a, b));
    }

    [Theory]
    [InlineData(10, 7, 3)]
    [InlineData(0, -5, 5)]
    public void Dif_ReturnsCorrectResult(int a, int b, int expected)
    {
        ICalculator calculator = Calculator.GenerateCalculator(CalculatorCode);
        Assert.Equal(expected, calculator.Dif(a, b));
    }

    [Theory]
    [InlineData(3, 4, 12)]
    [InlineData(-2, 6, -12)]
    public void Mul_ReturnsCorrectResult(int a, int b, int expected)
    {
        ICalculator calculator = Calculator.GenerateCalculator(CalculatorCode);
        Assert.Equal(expected, calculator.Mul(a, b));
    }

    [Theory]
    [InlineData(20, 5, 4)]
    [InlineData(15, -3, -5)]
    public void Div_ReturnsCorrectResult(int a, int b, int expected)
    {
        ICalculator calculator = Calculator.GenerateCalculator(CalculatorCode);
        Assert.Equal(expected, calculator.Div(a, b));
    }

    [Fact]
    public void Div_DivideByZeroException()
    {
        ICalculator calculator = Calculator.GenerateCalculator(CalculatorCode);
        Assert.Throws<DivideByZeroException>(() => calculator.Div(10, 0));
    }
}
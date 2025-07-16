namespace task14tests;
using task14;
using Xunit;

public class Task14Tests
{

    [Fact]
    public void Solve_ReturnsCorrectVavue()
    {
        static double X(double x) => x;
        static double SIN(double x) => Math.Sin(x);

        Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2), 1e-4);
        Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8), 1e-4);
        Assert.Equal(12.5, DefiniteIntegral.Solve(0, 5, X, 1e-6, 8), 1e-5); 
    }
}

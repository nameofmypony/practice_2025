namespace task11;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

public interface ICalculator
{
    int Sum(int a, int b);
    int Dif(int a, int b);
    int Mul(int a, int b);
    int Div(int a, int b);
}

public static class Calculator
{
    public static ICalculator GenerateCalculator(string classCode)
    {
        string modifiedCode = classCode.Replace(
            "public class Calculator",
            "public class Calculator : task11.ICalculator"
        );

        var compilation = CSharpCompilation.Create("DynamicCalculatorAssembly")
            .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            .AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
            )
            .AddSyntaxTrees(CSharpSyntaxTree.ParseText(modifiedCode));

        using var ms = new MemoryStream();
        compilation.Emit(ms);

        ms.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());
        var calculatorType = assembly.GetType("Calculator");
        return (ICalculator)Activator.CreateInstance(calculatorType);
    }
}
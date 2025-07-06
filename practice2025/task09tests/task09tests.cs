namespace task09tests;
using Xunit;
using System.Diagnostics;

public class DllAnalyzerTests
{
    [Fact]
    public void ConsoleDllAnalyzer_HasExpectedPrint()
    {
        var baseDir = AppContext.BaseDirectory;
        var solutionDir = Path.GetFullPath(Path.Combine(baseDir, "../../../../"));
        
        var projectPath = Path.Combine(solutionDir, "task09/task09.csproj");
        var dllPath = Path.Combine(solutionDir, "task07/bin/Debug/net9.0/task07.dll");

        string expected = "Class: DisplayNameAttribute\n" +
      "Method:\n" +
      "get_DisplayName\n" +
      "Method:\n" +
      "Equals\n" +
      "Parameters:\n" +
      "Object obj\n" +
      "Method:\n" +
      "GetHashCode\n" +
      "Method:\n" +
      "get_TypeId\n" +
      "Method:\n" +
      "Match\n" +
      "Parameters:\n" +
      "Object obj\n" +
      "Method:\n" +
      "IsDefaultAttribute\n" +
      "Method:\n" +
      "GetType\n" +
      "Method:\n" +
      "ToString\n" +
      "Attributes:\n" +
      "NullableContextAttribute\n" +
      "NullableAttribute\n" +
      "AttributeUsageAttribute\n" +
      "Constructors:\n" +
      ".ctor\n" +
      "Parameters:\n" +
      "String name\n\n" +
      "Class: VersionAttribute\n" +
      "Method:\n" +
      "get_Major\n" +
      "Method:\n" +
      "get_Minor\n" +
      "Method:\n" +
      "Equals\n" +
      "Parameters:\n" +
      "Object obj\n" +
      "Method:\n" +
      "GetHashCode\n" +
      "Method:\n" +
      "get_TypeId\n" +
      "Method:\n" +
      "Match\n" +
      "Parameters:\n" +
      "Object obj\n" +
      "Method:\n" +
      "IsDefaultAttribute\n" +
      "Method:\n" +
      "GetType\n" +
      "Method:\n" +
      "ToString\n" +
      "Attributes:\n" +
      "AttributeUsageAttribute\n" +
      "Constructors:\n" +
      ".ctor\n" +
      "Parameters:\n" +
      "Int32 major\n" +
      "Int32 minor\n\n" +
      "Class: SampleClass\n" +
      "Method:\n" +
      "TestMethod\n" +
      "Method:\n" +
      "get_Number\n" +
      "Method:\n" +
      "set_Number\n" +
      "Parameters:\n" +
      "String value\n" +
      "Method:\n" +
      "GetType\n" +
      "Method:\n" +
      "ToString\n" +
      "Method:\n" +
      "Equals\n" +
      "Parameters:\n" +
      "Object obj\n" +
      "Method:\n" +
      "GetHashCode\n" +
      "Attributes:\n" +
      "NullableContextAttribute\n" +
      "NullableAttribute\n" +
      "VersionAttribute\n" +
      "DisplayNameAttribute\n" +
      "Constructors:\n" +
      ".ctor\n\n" +
      "Class: ReflectionHelper\n" +
      "Method:\n" +
      "PrintTypeInfo\n" +
      "Parameters:\n" +
      "Type type\n" +
      "Method:\n" +
      "GetType\n" +
      "Method:\n" +
      "ToString\n" +
      "Method:\n" +
      "Equals\n" +
      "Parameters:\n" +
      "Object obj\n" +
      "Method:\n" +
      "GetHashCode\n\n" +
      "Class: <>c\n" +
      "Method:\n" +
      "GetType\n" +
      "Method:\n" +
      "ToString\n" +
      "Method:\n" +
      "Equals\n" +
      "Parameters:\n" +
      "Object obj\n" +
      "Method:\n" +
      "GetHashCode\n" +
      "Attributes:\n" +
      "SerializableAttribute\n" +
      "CompilerGeneratedAttribute\n" +
      "Constructors:\n" +
      ".ctor\n\n" +
      "Class: <>c__DisplayClass0_0\n" +
      "Method:\n" +
      "GetType\n" +
      "Method:\n" +
      "ToString\n" +
      "Method:\n" +
      "Equals\n" +
      "Parameters:\n" +
      "Object obj\n" +
      "Method:\n" +
      "GetHashCode\n" +
      "Attributes:\n" +
      "CompilerGeneratedAttribute\n" +
      "Constructors:\n" +
      ".ctor\n" +
      "\n";
        
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run --project \"{projectPath}\" -- \"{dllPath}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = solutionDir
        };

        using var process = new Process();
        process.StartInfo = startInfo;
        process.Start();
        process.WaitForExit();

        Assert.Equal(expected, process.StandardOutput.ReadToEnd());
    }
}
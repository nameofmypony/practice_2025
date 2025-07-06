namespace task09tests;
using Xunit;
using System.Diagnostics;

public class DllAnalyzerTests
{
    [Fact]
    public void ConsoleDllAnalyzer_HasExpectedPrint()
    {
        var baseDir = AppContext.BaseDirectory;
        var exePath = Path.Combine(baseDir, "../../../../task09/bin/Debug/net9.0/task09.exe");
        
        var projectPath = Path.GetFullPath(Path.Combine(baseDir, "../../../../task07/bin/Debug/net9.0/task07.dll"));
        
        string expected = "Class: DisplayNameAttribute\r\n" +
      "Method:\r\n" +
      "get_DisplayName\r\n" +
      "Method:\r\n" +
      "Equals\r\n" +
      "Parameters:\r\n" +
      "Object obj\r\n" +
      "Method:\r\n" +
      "GetHashCode\r\n" +
      "Method:\r\n" +
      "get_TypeId\r\n" +
      "Method:\r\n" +
      "Match\r\n" +
      "Parameters:\r\n" +
      "Object obj\r\n" +
      "Method:\r\n" +
      "IsDefaultAttribute\r\n" +
      "Method:\r\n" +
      "GetType\r\n" +
      "Method:\r\n" +
      "ToString\r\n" +
      "Attributes:\r\n" +
      "NullableContextAttribute\r\n" +
      "NullableAttribute\r\n" +
      "AttributeUsageAttribute\r\n" +
      "Constructors:\r\n" +
      ".ctor\r\n" +
      "Parameters:\r\n" +
      "String name\r\n\r\n" +
      "Class: VersionAttribute\r\n" +
      "Method:\r\n" +
      "get_Major\r\n" +
      "Method:\r\n" +
      "get_Minor\r\n" +
      "Method:\r\n" +
      "Equals\r\n" +
      "Parameters:\r\n" +
      "Object obj\r\n" +
      "Method:\r\n" +
      "GetHashCode\r\n" +
      "Method:\r\n" +
      "get_TypeId\r\n" +
      "Method:\r\n" +
      "Match\r\n" +
      "Parameters:\r\n" +
      "Object obj\r\n" +
      "Method:\r\n" +
      "IsDefaultAttribute\r\n" +
      "Method:\r\n" +
      "GetType\r\n" +
      "Method:\r\n" +
      "ToString\r\n" +
      "Attributes:\r\n" +
      "AttributeUsageAttribute\r\n" +
      "Constructors:\r\n" +
      ".ctor\r\n" +
      "Parameters:\r\n" +
      "Int32 major\r\n" +
      "Int32 minor\r\n\r\n" +
      "Class: SampleClass\r\n" +
      "Method:\r\n" +
      "TestMethod\r\n" +
      "Method:\r\n" +
      "get_Number\r\n" +
      "Method:\r\n" +
      "set_Number\r\n" +
      "Parameters:\r\n" +
      "String value\r\n" +
      "Method:\r\n" +
      "GetType\r\n" +
      "Method:\r\n" +
      "ToString\r\n" +
      "Method:\r\n" +
      "Equals\r\n" +
      "Parameters:\r\n" +
      "Object obj\r\n" +
      "Method:\r\n" +
      "GetHashCode\r\n" +
      "Attributes:\r\n" +
      "NullableContextAttribute\r\n" +
      "NullableAttribute\r\n" +
      "VersionAttribute\r\n" +
      "DisplayNameAttribute\r\n" +
      "Constructors:\r\n" +
      ".ctor\r\n\r\n" +
      "Class: ReflectionHelper\r\n" +
      "Method:\r\n" +
      "PrintTypeInfo\r\n" +
      "Parameters:\r\n" +
      "Type type\r\n" +
      "Method:\r\n" +
      "GetType\r\n" +
      "Method:\r\n" +
      "ToString\r\n" +
      "Method:\r\n" +
      "Equals\r\n" +
      "Parameters:\r\n" +
      "Object obj\r\n" +
      "Method:\r\n" +
      "GetHashCode\r\n\r\n" +
      "Class: <>c\r\n" +
      "Method:\r\n" +
      "GetType\r\n" +
      "Method:\r\n" +
      "ToString\r\n" +
      "Method:\r\n" +
      "Equals\r\n" +
      "Parameters:\r\n" +
      "Object obj\r\n" +
      "Method:\r\n" +
      "GetHashCode\r\n" +
      "Attributes:\r\n" +
      "SerializableAttribute\r\n" +
      "CompilerGeneratedAttribute\r\n" +
      "Constructors:\r\n" +
      ".ctor\r\n\r\n" +
      "Class: <>c__DisplayClass0_0\r\n" +
      "Method:\r\n" +
      "GetType\r\n" +
      "Method:\r\n" +
      "ToString\r\n" +
      "Method:\r\n" +
      "Equals\r\n" +
      "Parameters:\r\n" +
      "Object obj\r\n" +
      "Method:\r\n" +
      "GetHashCode\r\n" +
      "Attributes:\r\n" +
      "CompilerGeneratedAttribute\r\n" +
      "Constructors:\r\n" +
      ".ctor\r\n" +
      "\r\n";
        
        var startInfo = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = $"\"{projectPath}\"",
            RedirectStandardOutput = true,
        };

        using var process = new Process();
        process.StartInfo = startInfo;
        process.Start();
        process.WaitForExit();

        Assert.Equal(expected, process.StandardOutput.ReadToEnd());
    }
}
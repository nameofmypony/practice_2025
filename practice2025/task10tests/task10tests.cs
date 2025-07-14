namespace task10tests;
using task10;
using System.IO;
using System.Reflection;
using Xunit;

public class Task10Tests
{
    private readonly string _pluginsDirectory;

    public Task10Tests()
    {
        var binDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        _pluginsDirectory = Path.Combine(binDirectory, "..", "..", "..", "..", "plugins", "net9.0");
    }

    [Fact]
    public void Plugins_ShouldExecuteInDependencyOrder()
    {
        var consoleOut = new StringWriter();
        Console.SetOut(consoleOut);

        PluginSystem.Run(_pluginsDirectory);

        var output = consoleOut.ToString();
        Assert.Contains("Starting: plugin1", output);
        Assert.Contains("plugin1 message", output);
        Assert.Contains("Starting: plugin2", output);
        Assert.Contains("plugin2 message", output);

        var start1 = output.IndexOf("Starting: plugin1");
        var start2 = output.IndexOf("Starting: plugin2");
        Assert.True(start1 < start2);
    }

    [Fact]
    public void PluginWithoutDependencies_ShouldExecute()
    {
        var consoleOut = new StringWriter();
        Console.SetOut(consoleOut);

        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        File.Copy(
            Path.Combine(_pluginsDirectory, "plugin1.dll"),
            Path.Combine(tempDir, "plugin1.dll"),
            true
        );

        PluginSystem.Run(tempDir);

        var output = consoleOut.ToString();
        Assert.Contains("Starting: plugin1", output);
        Assert.Contains("plugin1 message", output);
        Assert.DoesNotContain("plugin2", output);

        Directory.Delete(tempDir, true);
    }
}

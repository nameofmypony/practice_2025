namespace task08tests;
using FileSystemCommands;
using System.IO;
using Xunit;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

        var command = new DirectorySizeCommand(testDir);
        var consoleOut = new StringWriter();
        Console.SetOut(consoleOut);

        command.Execute();

        Assert.Contains("bytes", consoleOut.ToString());
        Directory.Delete(testDir, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

        var command = new FindFilesCommand(testDir, "*.txt");
        var consoleOut = new StringWriter();
        Console.SetOut(consoleOut);

        command.Execute();

        Assert.Contains("test1.txt", consoleOut.ToString());
        Assert.Contains("test2.txt", consoleOut.ToString());
        Directory.Delete(testDir, true);
    }
}

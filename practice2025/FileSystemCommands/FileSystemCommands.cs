using CommandLib;

namespace FileSystemCommands;

public class DirectorySizeCommand(string path) : ICommand
{

    public void Execute()
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException();

        long size = Directory
            .EnumerateFiles(path, "*", SearchOption.AllDirectories)
            .Sum(f => new FileInfo(f).Length);

        Console.WriteLine($"Directory size: {size} bytes");
    }
}

public class FindFilesCommand(string path, string mask) : ICommand
{
    public void Execute()
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException();

        var files = Directory.EnumerateFiles(path, mask);
        Console.WriteLine($"Files: {string.Join(", ", files)}");
    }
}

using System.Reflection;
using CommandLib;

var commandsDll = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "FileSystemCommands.dll"
);

Assembly assembly = Assembly.LoadFrom(commandsDll);

CreateCommand(assembly, "DirectorySizeCommand", [@"C:\Test"]).Execute();
CreateCommand(assembly, "FindFilesCommand", [@"C:\Test", "*.txt"]).Execute();

static ICommand CreateCommand(Assembly assembly, string commandName, object[] args)
{
    Type? type = assembly.GetType(commandName)
        ?? throw new InvalidOperationException("Command not found");

    return (ICommand)Activator.CreateInstance(type, args)
        ?? throw new InvalidOperationException("Command creation failed");
}

namespace plugin0;

public interface IPlugin
{
    void Execute();
}

[AttributeUsage(AttributeTargets.Class)]
public class PluginLoad(string name, params string[] dependencies) : Attribute
{
    public string Name { get; } = name;
    public string[] Dependencies { get; } = dependencies;
}

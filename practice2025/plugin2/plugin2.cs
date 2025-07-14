namespace plugin2;
using plugin0;

[PluginLoad("plugin2", "plugin1")]
public class Plugin2 : IPlugin
{
    public void Execute() => Console.WriteLine("plugin2 message");
}

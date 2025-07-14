namespace plugin1;
using plugin0;

[PluginLoad("plugin1")]
public class Plugin1 : IPlugin
{
    public void Execute() => Console.WriteLine("plugin1 message");
}

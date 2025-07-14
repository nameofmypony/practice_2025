namespace task10;
using System.Reflection;
using plugin0;

public static class PluginSystem
{
    public static void RunFromAssemblies(IEnumerable<Assembly> assemblies)
    {
        var plugins = assemblies
            .SelectMany(asm => asm.GetTypes())
            .Where(t => t.IsPlugin())
            .ToList();
        var graph = plugins.ToDictionary(
            t => t.GetPluginName(),
            t => new PluginNode(t.GetPluginName(), t, t.GetDependencies())
        );
        graph.Values
            .SelectMany(node => node.Dependencies
                .Where(dep => graph.ContainsKey(dep))
                .Select(dep => (node, depNode: graph[dep])))
            .ToList()
            .ForEach(x => x.depNode.Dependants.Add(x.node));
        var processed = new HashSet<string>();
        ProcessNodes(graph.Values.Where(n => n.Dependencies.Count == 0), graph, processed);
    }
    
    private static void ProcessNodes(
        IEnumerable<PluginNode> ready, 
        IDictionary<string, PluginNode> graph,
        HashSet<string> processed)
    {
        ready.ToList().ForEach(node =>
        {
            if (processed.Contains(node.Name)) return;
            processed.Add(node.Name);
            Console.WriteLine($"Starting: {node.Name}");
            ((IPlugin)Activator.CreateInstance(node.Type)!).Execute();
            node.Dependants
                .Select(dependant => 
                {
                    dependant.Dependencies.Remove(node.Name);
                    return dependant;
                })
                .Where(dependant => dependant.Dependencies.Count == 0)
                .ToList()
                .ForEach(dependant => 
                    ProcessNodes([dependant], graph, processed));
        });
    }
    
    public static void Run(string directory = "plugins")
    {
        var assemblies = Directory.GetFiles(directory, "*.dll")
            .Select(Assembly.LoadFrom);
        RunFromAssemblies(assemblies);
    }
    
    private static bool IsPlugin(this Type t) =>
        typeof(IPlugin).IsAssignableFrom(t) &&
        t.GetCustomAttribute<PluginLoad>() != null;
    
    private static string GetPluginName(this Type t) => 
        t.GetCustomAttribute<PluginLoad>()!.Name;
    
    private static List<string> GetDependencies(this Type t) => 
        t.GetCustomAttribute<PluginLoad>()!.Dependencies.ToList();

    private class PluginNode(string name, Type type, List<string> dependencies)
    {
        public string Name { get; } = name;
        public Type Type { get; } = type;
        public List<string> Dependencies { get; } = dependencies;
        public List<PluginNode> Dependants { get; } = [];
    }
}

class Program
{
    static void Main() => PluginSystem.Run();
}
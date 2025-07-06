namespace task09;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Set the path to .dll");
            return;
        }
        
        string path = args[0];

        if (!File.Exists(path))
        {
            Console.WriteLine("File not found");
            return;
        }
        
        var asm = Assembly.LoadFrom(path);

        asm.GetTypes()
            .ToList()
            .ForEach(t =>
            {
                Console.WriteLine($"Class: {t.Name}");
                t.GetMethods().ToList().ForEach(m =>
                {
                    Console.WriteLine("Method:");
                    Console.WriteLine(m.Name);
                    if (m.GetParameters().Length == 0) return;
                    Console.WriteLine("Parameters:");
                    m.GetParameters().ToList().ForEach(p =>
                    {
                        Console.WriteLine($"{p.ParameterType.Name} {p.Name}");
                    });
                });

                if (t.GetCustomAttributes().Any())
                {
                    Console.WriteLine("Attributes:");
                    t.GetCustomAttributes().ToList().ForEach(m => Console.WriteLine(m.GetType().Name));
                }

                if (t.GetConstructors().Length != 0)
                {
                    Console.WriteLine("Constructors:");
                    t.GetConstructors().ToList().ForEach(c =>
                    {
                        Console.WriteLine(c.Name);
                        if (c.GetParameters().Length == 0) return;
                        Console.WriteLine("Parameters:");
                        c.GetParameters().ToList().ForEach(p =>
                        {
                            Console.WriteLine($"{p.ParameterType.Name} {p.Name}");
                        });
                    });
                }

                Console.WriteLine();
            });
    }
}

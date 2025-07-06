namespace task07;
using System.Reflection;
using System.Text;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class DisplayNameAttribute(string name) : Attribute
{
    public string DisplayName => name;
}

[AttributeUsage(AttributeTargets.Class)]
public class VersionAttribute(int major, int minor) : Attribute
{
    public int Major => major;
    public int Minor => minor;
}

[Version(1, 0)]
[DisplayName("Пример класса")]
public class SampleClass
{
    [DisplayName("Тестовый метод")]
    public void TestMethod() { }

    [DisplayName("Числовое свойство")]
    public string Number { get; set; }
}

public static class ReflectionHelper
{
    public static string PrintTypeInfo(Type type)
    {
        var info = new StringBuilder();
        var displayname = type.GetCustomAttribute<DisplayNameAttribute>();
        var version = type.GetCustomAttribute<VersionAttribute>();

        info.AppendLine($"Имя класса: {displayname?.DisplayName}");
        info.AppendLine($"Версия класса: {version?.Major}.{version?.Minor}\n");

        info.AppendLine("Методы класса:");
        type.GetMethods()
            .Select(m => m.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName)
            .Where(name => name != null)
            .ToList()
            .ForEach(name => info.AppendLine($"{name}"));

        info.AppendLine("Свойства класса:");
        type.GetProperties()
            .Select(p => p.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName)
            .Where(name => name != null)
            .ToList()
            .ForEach(name => info.AppendLine($"{name}"));
        
        Console.Write(info.ToString());
        
        return info.ToString();
    }
}

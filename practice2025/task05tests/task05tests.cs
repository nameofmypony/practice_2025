namespace task05tests;
using Xunit;
using task05;
public class TestClass
{
    public int PublicField;
    private string _privateField;
    public int Property { get; set; }

    public void Method() { }
    public void Method2(string a) { }
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods();

        Assert.Contains("Method", methods);
    }

    [Fact]
    public void GetMethodParams_ReturnsCorrectMethodParams()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methodParams = analyzer.GetMethodParams("Method2");

        Assert.Contains("a", methodParams);
    }

    [Fact]
    public void GetAllFields_IncludesPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields();

        Assert.Contains("_privateField", fields);
    }

    [Fact]
    public void GetProperties_ReturnsCorrectProperties()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var properties = analyzer.GetProperties();

        Assert.Contains("Property", properties);
    }

    [Fact]
    public void HasAttribute_ReturnsHasAttribute()
    {
        var analyzer = new ClassAnalyzer(typeof(AttributedClass));
        var hasSerializableAttribute = analyzer.HasAttribute<SerializableAttribute>();

        Assert.True(hasSerializableAttribute);
    }
}
namespace task05;
using System;
using System.Reflection;
using System.Collections.Generic;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }

    public IEnumerable<string> GetPublicMethods()
    {
        return _type.GetMethods().Select(m => m.Name).ToList();
    }

    public IEnumerable<string> GetMethodParams(string methodName)
    {
        return _type.GetMethod(methodName).GetParameters().Select(m => m.Name).ToList();
    }

    public IEnumerable<string> GetAllFields()
    {
        return _type.GetRuntimeFields().Select(m => m.Name).ToList();
    }

    public IEnumerable<string> GetProperties()
    {
        return _type.GetProperties().Select(m => m.Name).ToList();
    }
    
    public bool HasAttribute<T>() where T : Attribute
    {
        return _type.IsDefined(typeof(T));
    }
}
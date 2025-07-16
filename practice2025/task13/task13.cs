namespace task13;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
public class Subject
{
  public string Name {get; set; }
  public int Grade {get; set; }
}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Subject> Grades { get; set; }
}

public static class Serializer
{
    private class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string format = "yyyy-MM-dd";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(
                reader.GetString()!, 
                format, 
                CultureInfo.InvariantCulture
            );
        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(format));
        }
    }

    private static readonly JsonSerializerOptions options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new DateTimeConverter() }
    };

    public static string Serialize(Student student)
    {
        return JsonSerializer.Serialize(student, options);
    }

    public static Student Deserialize(string json)
    {
        var student = JsonSerializer.Deserialize<Student>(json, options);
        return student;
    }

    public static void Save(Student student, string path)
    {
        File.WriteAllText(path, Serialize(student));
    }

    public static Student Load(string path)
    {
        return Deserialize(File.ReadAllText(path));
    }
}
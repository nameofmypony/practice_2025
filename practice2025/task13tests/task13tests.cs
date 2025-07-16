namespace task13tests;

using task13;
using System.Text.Json;
public class SerializerTests
{
    private readonly Student test = new()
    {
        FirstName = "Иван",
        LastName = "Иванов",
        BirthDate = new DateTime(2000, 1, 15),
        Grades =
        [
            new() { Name = "Математика", Grade = 5 },
            new() { Name = "Физика", Grade = 4 }
        ]
    };

    [Fact]
    public void Serialize_IgnoresNulls()
    {
        var student = new Student { FirstName = "Петр" };
        var json = Serializer.Serialize(student);
        
        Assert.DoesNotContain("LastName", json);
        Assert.DoesNotContain("Grades", json);
    }

    [Fact]
    public void Serialize_UsesCustomDateFormat()
    {
        var json = Serializer.Serialize(test);
        
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        
        Assert.True(root.TryGetProperty("BirthDate", out var birthDateProperty));
        Assert.Equal("2000-01-15", birthDateProperty.GetString());
    }

    [Fact]
    public void FileOperations_WorkCorrectly()
    {
        var path = Path.GetTempFileName();
        
        Serializer.Save(test, path);
        var loaded = Serializer.Load(path);
        
        Assert.Equal(test.FirstName, loaded.FirstName);
        Assert.Equal(test.BirthDate, loaded.BirthDate);
        Assert.Equal(test.Grades[0].Name, loaded.Grades[0].Name);
        
        File.Delete(path);
    }
}
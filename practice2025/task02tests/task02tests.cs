namespace task02tests;
using task02;
public class StudentServiceTests
{
    private List<Student> _testStudents;
    private StudentService _service;

    public StudentServiceTests()
    {
        _testStudents = new List<Student>
        {
            new("Иван", "ФИТ", [5, 4, 5]),
            new("Анна", "ФИТ", [3, 4, 3]),
            new("Петр", "Экономика", [5, 5, 5])
        };
        _service = new StudentService(_testStudents);
    }

    //1
    [Fact]
    public void GetStudentsByFaculty_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsByFaculty("ФИТ").ToList();
        Assert.Equal(2, result.Count);
        Assert.True(result.All(s => s.Faculty == "ФИТ"));
    }

    //2
    [Fact]
    public void GetStudentsWithMinAverageGrade_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsWithMinAverageGrade(4).ToList();
        Assert.Equal(2, result.Count);
        Assert.True(result.All(s => s.Grades.Average() >= 4));
    }

    //3
    [Fact]
    public void GetStudentsOrderedByName_ReturnsOrderedStudents()
    {
        var result = _service.GetStudentsOrderedByName().ToList();
        Assert.Equal("Анна", result[0].Name);
        Assert.Equal("Иван", result[1].Name);
        Assert.Equal("Петр", result[2].Name);
    }

    //4
    [Fact]
    public void GroupStudentsByFaculty_ReturnsGroupedStudents()
    {
        var result = _service.GroupStudentsByFaculty();
        Assert.Equal(2, result.Count);
        Assert.Equal(2, result["ФИТ"].Count());
        Assert.Single(result["Экономика"]);
    }

    //5
    [Fact]
    public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
    {
        var result = _service.GetFacultyWithHighestAverageGrade();
        Assert.Equal("Экономика", result);
    }
}
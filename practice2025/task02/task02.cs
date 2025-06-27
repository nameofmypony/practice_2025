﻿namespace task02;

public class Student(string name, string faculty, List<int> grades)
{
    public string Name { get; } = name;
    public string Faculty { get; } = faculty;
    public List<int> Grades { get; } = grades;
}

public class StudentService
{
    private List<Student> _students;

    public StudentService(List<Student> students) => _students = students;

    // 1. Возвращает студентов указанного факультета
    public IEnumerable<Student> GetStudentsByFaculty(string faculty)
        => _students.Where(s => s.Faculty == faculty);

    // 2. Возвращает студентов со средним баллом >= minAverageGrade
    public IEnumerable<Student> GetStudentsWithMinAverageGrade(double minAverageGrade)
        => _students.Where(s => s.Grades.Average() >= minAverageGrade);

    // 3. Возвращает студентов, отсортированных по имени (A-Z)
    public IEnumerable<Student> GetStudentsOrderedByName()
        => _students.OrderBy(s => s.Name);

    // 4. Группировка по факультету
    public ILookup<string, Student> GroupStudentsByFaculty()
        => _students.ToLookup(s => s.Faculty);

    // 5. Находит факультет с максимальным средним баллом
    public string GetFacultyWithHighestAverageGrade()
        => _students
            .GroupBy(s => s.Faculty)
            .Select(t => new
            {
                Faculty = t.Key, 
                Average = t.SelectMany(s => s.Grades).Average()
            })
            .OrderByDescending(g => g.Average)
            .First().Faculty;
}
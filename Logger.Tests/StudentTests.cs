using Xunit;

namespace Logger.Tests;

public class StudentTests
{
    [Fact]
    public void Student_GraduationYearProperty_IsSetCorrectly()
    {
        // Arrange
        var fullName = new FullName("Alice", "B", "Johnson");
        var graduationYear = "2025";
        var student = new entities.Student(fullName, graduationYear);
        // Act & Assert
        Assert.Equal(graduationYear, student.GraduationYear);
    }

    [Fact]
    public void Student_NameProperty_ReturnsCorrectFullName()
    {
        // Arrange
        var fullName = new FullName("Bob", "C", "Williams");
        var graduationYear = "2024";
        var student = new entities.Student(fullName, graduationYear);
        // Act
        var name = student.Name;
        // Assert
        Assert.Equal("Bob C Williams", name);
    }


}

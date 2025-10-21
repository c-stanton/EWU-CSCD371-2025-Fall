using Xunit;

namespace Logger.Tests;

public class EmployeeTests
{
    [Fact]
    public void Employee_PositionProperty_IsSetCorrectly()
    {
        // Arrange
        var fullName = new FullName("John", "H", "Doe");
        var position = "Software Engineer";
        var employee = new entities.Employee(fullName, position);
        // Act & Assert
        Assert.Equal(position, employee.Position);
    }

    [Theory]
    [InlineData("Eric", "B", "Williams", "Director of Operations", "Eric B Williams")]
    [InlineData("Tara", (string)null, "Jones", "Marketing", "Tara Jones")]
    [InlineData("Sam", "", "Lee", "IT", "Sam Lee")]
    public void Employee_NameProperty_ReturnsFullNameFormat(string first, string middle, string last, string position, string expectedName)
    {
        // Arrange
        var fullName = new Logger.FullName(first, middle, last);
        var employee = new Logger.entities.Employee(fullName, position);
        // Assert
        Assert.Equal(expectedName, employee.Name);
    }

    [Fact]
    public void Employee_Equality_ReturnsFalseForSameContentDifferentId()
    {
        // Arrange
        var fullName = new Logger.FullName("John", "G", "Jones");
        var position = "Shift Lead";
        var employee1 = new Logger.entities.Employee(fullName, position);
        var employee2 = new Logger.entities.Employee(fullName, position);
        // Assert
        Assert.NotEqual(employee1.Id, employee2.Id);
        Assert.False(employee1.Equals(employee2));
    }

    [Fact]
    public void Employee_Equality_ReturnsTrueForSameIdAndContent()
    {
        // Arrange
        var sharedId = Guid.NewGuid();
        var fullName = new Logger.FullName("Same", null, "Guy");
        var position = "Same Pos";
        // Act
        var employee1 = new Logger.entities.Employee(fullName, position) { Id = sharedId };
        var employee2 = new Logger.entities.Employee(fullName, position) { Id = sharedId };
        // Assert
        Assert.True(employee1.Equals(employee2));
        Assert.True(employee1 == employee2);
        Assert.Equal(employee1.GetHashCode(), employee2.GetHashCode());
    }

    [Fact]
    public void Employee_WithExpression_CreatesNewRecordWithModifiedPosition()
    {
        // Arrange
        var originalPosition = "Department Lead";
        var originalFullName = new Logger.FullName("Derrick", "M", "Smith");
        var originalEmployee = new Logger.entities.Employee(originalFullName, originalPosition);

        // Act
        var newPosition = "Department Manager";
        var newEmployee = originalEmployee with { Position = newPosition };

        // Assert
        Assert.Equal(originalPosition, originalEmployee.Position);
        Assert.Equal(newPosition, newEmployee.Position);
        Assert.NotSame(originalEmployee, newEmployee);
    }
}

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

    [Fact]
    public void Employee_NameProperty_ReturnsCorrectFullName()
    {
        // Arrange
        var fullName = new FullName("Jane", "A", "Smith");
        var position = "Product Manager";
        var employee = new entities.Employee(fullName, position);
        // Act
        var name = employee.Name;
        // Assert
        Assert.Equal("Jane A Smith", name);
    }
}

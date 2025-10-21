using Xunit;

namespace Logger.Tests;

public class BookTests
{
    [Fact]
    public void Book_NameProperty_ReturnsCorrectFormat()
    {
        // Arrange
        var title = "The Great Gatsby";
        var author = "F. Scott Fitzgerald";
        var book = new entities.Book(title, author);
        // Act
        var name = book.Name;
        // Assert
        Assert.Equal("The Great Gatsby by F. Scott Fitzgerald", name);
    }
}

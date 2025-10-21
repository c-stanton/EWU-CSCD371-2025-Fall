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

    [Fact]
    public void Book_TitleProperty_IsSetCorrectly()
    {
        // Arrange
        var title = "1984";
        var author = "George Orwell";
        var book = new entities.Book(title, author);
        // Act & Assert
        Assert.Equal(title, book.Title);
    }

    [Fact]
    public void Book_AuthorProperty_IsSetCorrectly()
    {
        // Arrange
        var title = "To Kill a Mockingbird";
        var author = "Harper Lee";
        var book = new entities.Book(title, author);
        // Act & Assert
        Assert.Equal(author, book.Author);
    }
}

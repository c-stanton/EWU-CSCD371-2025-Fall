using System;
using Xunit;

namespace Logger.Tests.StorageTests;

public class TestEntity : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    Guid IEntity.Id { get => Id; init => throw new NotImplementedException(); }
    string IEntity.Name => throw new NotImplementedException();
}

public class StorageTests
{
    [Fact]
    public void Add_Entity_ShouldBeContained()
    {
        // Arrange
        var storage = new Storage();
        storage.Add(entity);
        return storage;
    }

        // Act
        storage.Add(entity);

        // Assert
        Assert.True(storage.Contains(entity));
    }

    [Fact]
    public void Remove_Entity_ShouldNotBeContained()
    {
        // Arrange
        var storage = new Storage();
        var entity = new TestEntity();
        storage.Add(entity);

        // Act
        storage.Remove(entity);

        // Assert
        Assert.False(storage.Contains(entity));
    }

    [Fact]
    public void Contains_EntityNotAdded_ShouldReturnFalse()
    {
        // Arrange
        var storage = new Storage();
        var entity = new TestEntity();

        // Act & Assert
        Assert.False(storage.Contains(entity));
    }

    [Fact]
    public void Get_ExistingEntityById_ShouldReturnEntity()
    {
        // Arrange
        var storage = new Storage();
        var entity = new TestEntity();
        storage.Add(entity);

        // Act
        var retrieved = storage.Get(entity.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal(entity, retrieved);
    }
    
    [Fact]
    public void Get_NonExistingId_ShouldReturnNull()
    {
        // Arrange
        var storage = new Storage();
        var entity = new TestEntity();
        storage.Add(entity);
        var randomId = Guid.NewGuid();

        // Act
        var retrieved = storage.Get(randomId);

        // Assert
        Assert.Null(retrieved);
    }

    [Fact]
    public void Add_MultipleEntities_ShouldContainAll()
    {
        // Arrange
        var storage = new Storage();
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        // Act
        storage.Add(entity1);
        storage.Add(entity2);

        // Assert
        Assert.True(storage.Contains(entity1));
        Assert.True(storage.Contains(entity2));
    }

    [Fact]
    public void Remove_EntityNotAdded_ShouldNotThrow()
    {
        // Arrange
        var storage = new Storage();
        var entity = new TestEntity();

        // Act & Assert
        var exception = Record.Exception(() => storage.Remove(entity));
        Assert.Null(exception); 
    }
}

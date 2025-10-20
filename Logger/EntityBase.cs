using System;
using System.Diagnostics.CodeAnalysis;

namespace Logger;

public abstract record class EntityBase : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public abstract string Name { get; }
    protected EntityBase(Guid id)
    {
        Id = id;
    }
    protected EntityBase()
    {
    }
}
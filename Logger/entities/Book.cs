using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.entities;

public record class Book(string Title, string? Subtitle, FullName? Author)
{
    public Guid Id { get; init; } = Guid.NewGuid();

    // Calculated property — no backing field.
    public string Name => string.IsNullOrWhiteSpace(Subtitle) ? Title : $"{Title}: {Subtitle}";
}

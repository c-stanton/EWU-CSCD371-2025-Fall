using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.entities;

// The Name property is implemented implicitly because it should be publicly accessible as part of the class's API.
public record class Book : EntityBase
{
    public string Title { get; init; }
    public string Author { get; init; }
    public override string Name => $"{Title} by {Author}";

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
}

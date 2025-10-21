using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.entities;

public record class Book : EntityBase
{
    public string Title { get; init; }
    public string Author { get; init; }
    public override string Name => $"{Title} by {Author}";

    public Book(string title, string author)
    {
        this.Title = title;
        this.Author = author;
    }
}

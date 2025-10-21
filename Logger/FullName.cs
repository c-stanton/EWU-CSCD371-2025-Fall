using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger;

public record class FullName
{
    public string First { get; init; }
    public string Middle { get; init; }
    public string Last { get; init; }
    public FullName(string first, string middle, string last)
    {
        First = first;
        Middle = middle;
        Last = last;
    }
}

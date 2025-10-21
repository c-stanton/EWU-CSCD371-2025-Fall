using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace Logger.entities;

public record class Employee : Person
{
    public string Position { get; init; }
    
    public Employee(FullName fullName, string position) : base(fullName)
    {
        Position = position;
    }
}

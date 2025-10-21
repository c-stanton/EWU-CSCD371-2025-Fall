using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace Logger.entities;

// Student class inherits the IEntity implementation from Person. So both interface members are inherited implicitly
// and are publicly accessible as part of the class's API.
public record class Student : Person
{ 
    public string GraduationYear { get; set; }

    public Student(FullName fullName, string graduationYear) : base(fullName)
    {
        GraduationYear = graduationYear;
    }

}

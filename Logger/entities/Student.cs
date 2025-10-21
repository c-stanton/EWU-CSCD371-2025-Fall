using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace Logger.entities;

public record class Student : Person
{ 
    public string GraduationYear { get; set; }

    public Student(FullName fullName, string graduationYear) : base(fullName)
    {
        GraduationYear = graduationYear;
    }

}

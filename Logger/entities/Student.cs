using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace Logger.entities
{
    public record class Student(FullName FullName, string StudentID)
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        // Calculated property — formats FullName and appends student number.
        public string Name =>
            FullName is null
                ? $"Student #{StudentID}"
                : string.IsNullOrWhiteSpace(FullName.MiddleName)
                    ? $"{FullName.LastName}, {FullName.FirstName} (#{StudentID})"
                    : $"{FullName.LastName}, {FullName.FirstName} {FullName.MiddleName} (#{StudentID})";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace Logger.entities
{
    public record class Employee(FullName FullName, string EmployeeId)
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        private string FormatName() =>
            FullName is null
                ? string.Empty
                : string.IsNullOrWhiteSpace(FullName.MiddleName)
                    ? $"{FullName.LastName}, {FullName.FirstName}"
                    : $"{FullName.LastName}, {FullName.FirstName} {FullName.MiddleName}";

        // Calculated property — no backing field
        public string Name => FormatName();
    }
}

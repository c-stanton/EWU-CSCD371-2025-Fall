using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public abstract record Person : EntityBase
    {
        public FullName FullName { get; init; }

        public override string Name => FullName.ToString();

        public Person(FullName fullName)
        {
            FullName = fullName;
        }
    }
}

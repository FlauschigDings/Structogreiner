using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.Inline
{
    class VariableAssignment : Inline
    {
        public string Name { get; }
        public string Value { get; }

        public VariableAssignment(string name, string value) : base("instruction", $"{name} <- {value}")
        {
            Name = name;
            Value = value;
        }
    }
}

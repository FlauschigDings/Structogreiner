using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter.Instructions
{
    internal class VariableAssignment : XmlInstruction
    {
        public string Name { get; }
        public string Value { get; }

        public VariableAssignment(string name, string value) : base($"{name} <- {value}", "", 0, 0)
        {
            Name = name;
            Value = value;
        }
    }
}

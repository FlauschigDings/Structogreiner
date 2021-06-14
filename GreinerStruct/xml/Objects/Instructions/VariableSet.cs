using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter.xml.Instructions
{
    internal class VariableSet : XmlInstruction
    {
        public string Name { get; }
        public string Value { get; }

        public VariableSet(string name, string value) : base($"{name}<- {value}", "", 0, 0)
        {
            Name = name;
            Value = value;
        }
    }
}

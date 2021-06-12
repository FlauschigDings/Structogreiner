using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter.xml.Instructions
{
    internal class VariableDeclaration : XmlInstruction
    {
        public string Name { get; }
        public Type Type { get; }

        public VariableDeclaration(string name, Type type) : base($"{name}: {type}", "", 0, 0)
        {
            Name = name;
            Type = type;
        }
    }
}

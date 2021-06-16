using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.Inline
{
    internal class VariableDeclaration : Inline
    {
        public string Name { get; }
        public Type Type { get; }

        public VariableDeclaration(string name, Type type) : base("instruction", $"{name}: {type}")
        {
            Name = name;
            Type = type;
        }

        public override string ToString() => $"{Name}: {Type}";
    }
}

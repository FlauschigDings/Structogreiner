using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter.xml.Instructions
{
    internal class Function : XmlInstruction
    {
        public ImmutableList<VariableDeclaration> Variables { get; }

        public Function(IList<VariableDeclaration> variables) : base(null, "", 0, 0)
        {
            Variables = variables.ToImmutableList();
        }
    }
}

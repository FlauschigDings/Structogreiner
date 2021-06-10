using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Operations
{
    internal class VariableDeclarationInstruction : Instruction
    {
        public string Name { get; }

        public Type Type { get;  }
    }
}

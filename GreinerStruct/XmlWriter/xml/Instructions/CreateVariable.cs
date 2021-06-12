using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter.Instructions
{
    class CreateVariable : XmlInstruction
    {
        public CreateVariable(string variablename, string variableType) : base(variablename + " <- " + variableType, "", 0, 0) {}
    }
}

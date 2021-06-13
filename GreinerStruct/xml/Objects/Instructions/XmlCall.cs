using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{
    class XmlCall : XmlInstruction
    {
        public XmlCall(string methodName) : base($"\"{methodName}()\"", "", 0, 0, "call") { }
    }
}

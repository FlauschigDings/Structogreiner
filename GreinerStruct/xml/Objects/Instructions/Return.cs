using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{
    class Return : XmlInstruction
    {
        public Return(string text) : base($"RückgabeWert: {text}", type: "jump") { }
    }
}

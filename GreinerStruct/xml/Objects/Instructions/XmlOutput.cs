using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{
    class Output : XmlInstruction
    {
        public Output(string output) : base($"AUSGABE DIALOG: \"{output}\"", "", 0, 0) { }
    }
}

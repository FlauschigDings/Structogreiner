using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.xml.Objects.Inline
{
    class Output : Inline
    {
        public Output(string output) : base("instruction", $"AUSGABE DIALOG: \"{output}\"") { }
    }
}

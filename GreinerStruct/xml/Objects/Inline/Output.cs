using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.Inline
{
    internal class Output : Inline
    {
        public Output(string output) : base("instruction", $"{Program.i18n.Output()}: {output}") { }
    }
}

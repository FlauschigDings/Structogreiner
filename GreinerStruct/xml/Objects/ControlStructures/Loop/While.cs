using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.xml.Objects.ControlStructures.Loop
{
    class While : Loops
    {
        public While(string ifValue) : base("while")
        {
            this.qLoops = new QWhile();
            this.AddAttribute("text", $"{ifValue} (?)");
            this.AddAttribute("commit", "");
            this.AddAttribute("disable", "0");
        }

        private class QWhile : Loops.QLoops
        {
            public QWhile() : base("qWhile") { }
        }
    }
}

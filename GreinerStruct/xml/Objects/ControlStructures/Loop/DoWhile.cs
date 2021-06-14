using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.ControlStructures.Loop
{
    class DoWhile : Loops
    {
        public DoWhile(string ifValue) : base("repeat") {
            this.qLoops = new QDoWhile();
            this.AddAttribute("text", $"{ifValue} (?)");
            this.AddAttribute("commit", "");
            this.AddAttribute("disable", "0");
        }

        private class QDoWhile : Loops.QLoops
        {
            public QDoWhile() : base("qRepeat") { }
        }
    }
}

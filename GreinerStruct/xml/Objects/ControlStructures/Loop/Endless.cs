using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.ControlStructures.Loop
{
    class Endless : Loops
    {
        public Endless() : base("forever")
        {
            this.qLoops = new QEndless();
            this.AddAttribute("commit", "");
            this.AddAttribute("disable", "0");
        }

        private class QEndless : Loops.QLoops
        {
            public QEndless() : base("qForever") { }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.ControlStructures.Loop
{
    internal class Endless : LoopBase
    {
        public Endless() : base("forever")
        {
            this.qLoops = new QEndless();
            this.AddAttribute("commit", "");
            this.AddAttribute("disable", "0");
        }

        private class QEndless : LoopBase.QLoops
        {
            public QEndless() : base("qForever") { }
        }
    }
}

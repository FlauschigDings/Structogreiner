using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{

    class XmlInstruction : XmlWriter
    {
        public XmlInstruction(string text, string comment, int rotated, int disable) : base("instruction")
        {
            this.AddAttribute("text", text);
            this.AddAttribute("comment", comment);
            this.AddAttribute("rotated", rotated);
            this.AddAttribute("disable", disable);
        }
    }
}

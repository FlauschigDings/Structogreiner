using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{

    class XmlInstruction : XmlObject
    {
        public XmlInstruction(string text, string comment = "", int rotated = 0, int disable = 0, string type = "instruction") : base(type)
        {
            this.AddAttribute("text", $"\"{text.Replace("\"", "\"\"")}\"");
            this.AddAttribute("comment", comment);
            this.AddAttribute("rotated", rotated);
            this.AddAttribute("disable", disable);
        }
    }
}

using Structogreiner.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structogreiner.Xml.Objects.Inline
{
    internal class Inline : XmlObject
    {
        // "instruction"
        public Inline(string type, string text, string comment = "", int rotated = 0, int disable = 0) : base(type)
        {
            this.AddAttribute("text", $"\"{text.Replace("\"", "\"\"")}\"");
            this.AddAttribute("comment", comment);
            this.AddAttribute("rotated", rotated);
            this.AddAttribute("disable", disable);
        }
    }
}

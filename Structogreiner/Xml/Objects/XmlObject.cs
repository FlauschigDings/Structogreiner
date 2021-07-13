using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structogreiner.Xml
{
    internal class XmlObject : XmlWriter
    {
        public XmlObject(string elementName) : base(elementName)
        {
            this.AddAttribute("color", "ffffff");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GreinerStruct.XmlWriter
{
    class XmlRootWriter
    {

        private readonly XmlElement _xml;

        public XmlRootWriter()
        {
            XmlElement myElement = new XmlDocument().CreateElement("root", "ns");
        }

    }
}

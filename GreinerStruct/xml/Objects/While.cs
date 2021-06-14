using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{
    class While : XmlObject
    {
        public While(string elementName) : base("while")
        {
        }

        private class XmlqFor : XmlObject
        {
            public XmlqFor() : base("qWhile") { }

            public XmlqFor AddXmlObject<T>(T t) where T : XmlObject
            {
                this.AddInnerXml(t);
                return this;
            }
        }
    }
}

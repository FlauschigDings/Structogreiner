using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{
    internal class XmlChildren : XmlWriter
    {
        public XmlChildren() : base("children") { }

        public void AddXmlObject<T>(T s) where T: XmlObject => this.AddInnerXml(s);

        public override string ToString() =>  this.XmlString();
    }
}

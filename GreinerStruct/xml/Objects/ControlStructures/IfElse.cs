using GreinerStruct.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.ControlStructures
{
    internal class IfElse : XmlObject
    {
        private readonly XmlQValue[] xmlqValue;
        public IfElse(string ifValue) : base("alternative")
        {
            xmlqValue = new XmlQValue[2];
            XmlqTrue xmlqTrue = new XmlqTrue();
            XmlqFalse xmlqFalse = new XmlqFalse();

            xmlqValue[0] = xmlqTrue;
            xmlqValue[1] = xmlqFalse;

            this.AddAttribute("text", $"{ifValue} ?");
            this.AddAttribute("comment", "");
            this.AddAttribute("disabled", "0");

            this.SetInnerXml(xmlqValue);
        }

        public IfElse AddXmlObject<T>(bool val, T t) where T : XmlObject
        {
            if (val) this.xmlqValue[0].AddXmlObject(t);
            else this.xmlqValue[1].AddXmlObject(t);
            this.SetInnerXml(this.xmlqValue);
            return this;
        }

        private class XmlQValue : XmlObject
        {
            public XmlQValue(string elementName) : base(elementName) { }

            public virtual XmlQValue AddXmlObject<T>(T t) where T : XmlObject
            {
                this.AddInnerXml(t);
                return this;
            }
        }

        private class XmlqTrue : XmlQValue
        {
            public XmlqTrue() : base("qTrue") { }

            public override XmlqTrue AddXmlObject<T>(T t)
            {
                this.AddInnerXml(t);
                return this;
            }
        }

        private class XmlqFalse : XmlQValue
        {
            public XmlqFalse() : base("qFalse") { }

            public override XmlqFalse AddXmlObject<T>(T t)
            {
                this.AddInnerXml(t);
                return this;
            }
        }
    }
}

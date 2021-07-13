using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Structogreiner.Xml
{
    internal class XmlWriter
    {
        protected readonly XmlElement xml;

        public XmlWriter(string elementName) => this.xml = new XmlDocument().CreateElement(elementName);
        public void AddAttribute<T>(string key, T value) => this.xml.SetAttribute(key, value.ToString());
        public void SetInnerXml<T>(T value) where T : XmlWriter => this.xml.InnerXml = value.XmlString();
        public void AddInnerXml<T>(T value) where T : XmlWriter => this.xml.InnerXml += value.XmlString();

        public void SetInnerXml<T>(T[] values) where T : XmlWriter
        {
            var val = new StringBuilder();

            foreach (var value in values)
            {
                val.Append(value.XmlString());
            }

            this.xml.InnerXml = val.ToString();
        }

        public string XmlString() => this.xml.OuterXml;
        public XmlElement Xml() => this.xml;
    }
}

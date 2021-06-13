using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{
    class XmlFor : XmlObject
    {

        private readonly XmlqFor xmlqFor;

        public XmlFor(string variableName, int startValue, int endValue, int stepConst) : base("for")
        {
            this.xmlqFor = new XmlqFor();
            this.AddAttribute("text", $"{variableName}: AW={startValue}, EW={endValue}, SW={stepConst}");
            this.AddAttribute("comment", "");
            this.AddAttribute("counterVar", variableName);
            this.AddAttribute("startValue", startValue);
            this.AddAttribute("endValue", endValue);
            this.AddAttribute("stepConst", stepConst);
            this.AddAttribute("style", "COUNTER");
            this.AddAttribute("color", "ffffff");
            this.AddAttribute("disabled", "0");

        }

        public XmlFor AddXmlObject<T>(T t) where T : XmlObject
        {
            this.xmlqFor.AddXmlObject(t);
            this.SetInnerXml(this.xmlqFor);
            return this;
        }

        // idk why they do it that way 
        private class XmlqFor : XmlObject
        {
            public XmlqFor() : base("qFor") 
            {
                this.AddAttribute("color", "ffffff");
            }

            public XmlqFor AddXmlObject<T>(T t) where T : XmlObject
            {
                this.AddInnerXml(t);
                return this;
            }
        }

    }
}

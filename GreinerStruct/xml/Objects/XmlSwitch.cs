using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{
    class XmlSwitch : XmlObject
    {
        private readonly XmlqCase[] xmlqCase;

        public XmlSwitch(string value, params string[] parm) : base("case")
        {
            xmlqCase = new XmlqCase[parm.Length];
            for(int i = 0; i < parm.Length;i++) xmlqCase[i] = new XmlqCase();
            this.SetInnerXml(this.xmlqCase);

            var args = new StringBuilder();
            args.Append($"\"{value}\",");
            parm.ToList().ForEach(e => args.Append($"\"{e}\","));


            this.AddAttribute("text", args.ToString().Substring(0, args.Length-1));
            this.AddAttribute("comment", "");
            this.AddAttribute("style", "COUNTER");
            this.AddAttribute("color", "ffffff");
            this.AddAttribute("disabled", "0");
        }

        public XmlSwitch AddXmlObject<T>(int index, T t) where T : XmlObject
        {
            this.xmlqCase[index].AddXmlObject(t);
            this.SetInnerXml(this.xmlqCase);
            return this;
        }

        private class XmlqCase : XmlObject
        {
            public XmlqCase() : base("qCase")
            {
                this.AddAttribute("color", "ffffff");
            }

            public XmlqCase AddXmlObject<T>(T t) where T : XmlObject
            {
                this.AddInnerXml(t);
                return this;
            }
        }

    }
}

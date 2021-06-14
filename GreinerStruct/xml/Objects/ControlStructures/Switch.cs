using GreinerStruct.XmlWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.ControlStructures
{
    internal class Switch : XmlObject
    {
        private readonly QCase[] xmlqCase;

        public Switch(string value, params string[] parm) : base("case")
        {
            xmlqCase = new QCase[parm.Length];
            for(int i = 0; i < parm.Length;i++) xmlqCase[i] = new QCase();
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

        public Switch AddXmlObject<T>(T[] t) where T : XmlObject
        {
            for (var i = 0; i < t.Length; i++)
            {
                this.xmlqCase[i].AddXmlObject(t[i]);
                this.SetInnerXml(this.xmlqCase);
            }
            return this;
        }

        private class QCase : XmlObject
        {
            public QCase() : base("qCase")
            {
                this.AddAttribute("color", "ffffff");
            }

            public QCase AddXmlObject<T>(T t) where T : XmlObject
            {
                this.AddInnerXml(t);
                return this;
            }
        }
    }
}

using System.Text;

namespace Structogreiner.Xml.Objects.ControlStructures
{
    internal class Switch : XmlObject
    {
        private readonly QCase[] xmlqCase;

        public Switch(string value, params string[] parms) : base("case")
        {
            xmlqCase = new QCase[parms.Length];
            for (int i = 0; i < parms.Length; i++)
            {
                xmlqCase[i] = new QCase();
            }
            this.SetInnerXml(this.xmlqCase);

            var args = new StringBuilder();
            args.Append($"\"{value}\",");
            foreach (var parm in parms)
            {
                args.Append($"\"{parm}\",");
            }

            this.AddAttribute("text", args.ToString().Substring(0, args.Length - 1));
            this.AddAttribute("comment", "");
            this.AddAttribute("style", "COUNTER");
            this.AddAttribute("color", "ffffff");
            this.AddAttribute("disabled", "0");
        }

        public Switch AddXmlObject<T>(int pos,T t) where T : XmlObject
        {
            this.xmlqCase[pos].AddXmlObject(t);
            this.SetInnerXml(this.xmlqCase);
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

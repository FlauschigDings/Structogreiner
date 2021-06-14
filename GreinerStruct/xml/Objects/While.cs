using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{
    class While : XmlObject
    {

        private readonly QWhile qWhile;


        public While(string ifValue) : base("while")
        {
            qWhile = new QWhile();
            this.AddAttribute("text", $"{ifValue} (?)");
            this.AddAttribute("commit", "");
            this.AddAttribute("disable", "0");
        }

        public While AddXmlObject<T>(T t) where T : XmlObject
        {
            this.qWhile.AddXmlObject(t);
            this.SetInnerXml(this.qWhile);
            return this;
        }

        private class QWhile : XmlObject
        {
            public QWhile() : base("qWhile") { }

            public QWhile AddXmlObject<T>(T t) where T : XmlObject
            {
                this.AddInnerXml(t);
                return this;
            }
        }
    }
}

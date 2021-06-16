using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.ControlStructures.Loops
{

    internal class Foreach : XmlObject
    {
        private readonly QForeach qFor;

        public Foreach(string variableName, string array) : base("for")
        {
            this.qFor = new QForeach();
            this.AddAttribute("text", $"{variableName}: {Program.i18n.ForStartValue()}={1}, {Program.i18n.ForEndValue()}={$"{array}.Length"}, {Program.i18n.ForStepConst()}={1}");
            this.AddAttribute("comment", "");
            this.AddAttribute("counterVar", variableName);
            this.AddAttribute("startValue", 1);
            this.AddAttribute("endValue", 0);
            this.AddAttribute("stepConst", 1);
            this.AddAttribute("style", "COUNTER");
            this.AddAttribute("color", "ffffff");
            this.AddAttribute("disabled", "0");
        }

        public Foreach AddXmlObject<T>(T t) where T : XmlObject
        {
            this.qFor.AddXmlObject(t);
            this.SetInnerXml(this.qFor);
            return this;
        }

        // idk why they do it that way 
        private class QForeach : XmlObject
        {
            public QForeach() : base("qFor")
            {
                this.AddAttribute("color", "ffffff");
            }

            public QForeach AddXmlObject<T>(T t) where T : XmlObject
            {
                this.AddInnerXml(t);
                return this;
            }
        }
    }
}

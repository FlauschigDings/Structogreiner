using GreinerStruct.Xml.Objects.Inline;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.XmlWriter
{
    enum MethodType
    {
        Main,
        Sub,
        Includable
    }

    class XmlRoot : XmlWriter
    {
        public XmlChildren children { get; set; }
        public string title { get; set; }

        public XmlRoot(string title, string author, List<VariableDeclaration> variables, List<VariableDeclaration> parameters, Type returnType = null, MethodType type = MethodType.Main, string comment = "created by GreinerStruct") : base("root")
        {
            this.children = new XmlChildren();
            var created = DateTime.Now;
            this.title = title;
            title = Translate(title, type, variables, parameters, returnType);

            // Check if the type is sus AMOGUS!

            //--------------------------------- Default Values -------------------------------------
            this.AddAttribute("xmlns:nsd", "https://structorizer.fisch.lu");
            this.AddAttribute("version", "3.30-14");
            this.AddAttribute("preRepeat", "until ");
            this.AddAttribute("postFor", "to");
            this.AddAttribute("preReturn", "return");
            this.AddAttribute("postForIn", "in");
            this.AddAttribute("preWhile", "while ");
            this.AddAttribute("output", "OUTPUT");
            this.AddAttribute("input", "INPUT");
            this.AddAttribute("preFor", "for");
            this.AddAttribute("preExit", "exit");
            this.AddAttribute("preLeave", "leave");
            this.AddAttribute("ignoreCase", "true");
            this.AddAttribute("preThrow", "throw");
            this.AddAttribute("preForIn", "foreach");
            this.AddAttribute("stepFor", "by");
            this.AddAttribute("origin", "Structorizer 3.30-14");
            this.AddAttribute("style", "nice");
            this.AddAttribute("color", "ffffff");
            //--------------------------------- Default Values -------------------------------------
            this.AddAttribute("author", author);
            this.AddAttribute("created", created);
            this.AddAttribute("changed", created);
            this.AddAttribute("text", title);
            this.AddAttribute("comment", comment);
            this.AddAttribute("type", type.ToString().ToLower());
        }

        public XmlRoot AddXmlObject<T>(T s) where T : XmlObject {
            this.children.AddXmlObject(s);
            this.SetInnerXml(this.children);
            return this;
        }

        private string Translate(string title, MethodType type, List<VariableDeclaration> variables, List<VariableDeclaration> parameters, Type returnType)
        {

            if (type is MethodType.Sub)
            {
                const string inputParmsTitle = "Eingabe Parameter:";
                var inputParms = string.Join(",", parameters.Select(p => $"{p}"));

                const string varsTitle = "Variable Deklarationen:";
                var varsParam = string.Join(",", variables.Select(v => $"{v.Name}: {v.Type}"));

                var returnParmTitle = "";
                var returnParm = "";

                if (returnType is not null)
                {

                    returnParmTitle = "Rückgabe Parameter:";
                    returnParm = $" {returnType}";
                }

                return $"\"{title}\", \"{inputParmsTitle}\", \"{inputParms}\", \"{varsTitle}\", \"{varsParam}\", \"{returnParmTitle}\", \"{returnParm}\"";
            }
            variables.ForEach(e => this.AddXmlObject(e));
            return $"{title}";
        }
    }
}

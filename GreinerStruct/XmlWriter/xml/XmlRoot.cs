﻿using GreinerStruct.XmlWriter.Instructions;
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

        public XmlRoot(string title, string author, ImmutableList<VariableDeclaration> variables, VariableDeclaration returnType = null, MethodType type = MethodType.Main, string comment = "created by GreinerStruct") : base("root")
        {
            // Check if the type is sus AMOGUS!
            if(type is MethodType.Sub)
            {
                var savetile = title;



            }

            this.children = new XmlChildren();
            var created = DateTime.Now;
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
            this.AddAttribute("type", type.ToString());
        }

        public void AddXmlObject<T>(T s) where T : XmlInstruction {
            this.children.AddXmlObject(s);
            this.SetInnerXml(this.children);
        }
    }
}

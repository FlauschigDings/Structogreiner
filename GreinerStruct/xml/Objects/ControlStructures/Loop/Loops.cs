﻿using GreinerStruct.XmlWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.xml.Objects.ControlStructures.Loop
{
    class Loops : XmlObject
    {
        protected QLoops qLoops;

        public Loops(string elementname) : base(elementname) {
        }

        public Loops AddXmlObject<T>(T t) where T : XmlObject
        {
            this.qLoops.AddXmlObject(t);
            this.SetInnerXml(this.qLoops);
            return this;
        }

        protected class QLoops : XmlObject
        {
            public QLoops(string elementname) : base(elementname) { }

            public QLoops AddXmlObject<T>(T t) where T : XmlObject
            {
                this.AddInnerXml(t);
                return this;
            }
        }
    }
    
}
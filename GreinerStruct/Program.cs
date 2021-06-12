using GreinerStruct.XmlWriter;
using GreinerStruct.XmlWriter.Instructions;
using GreinerStruct.XmlWriter.xml.Instructions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GreinerStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer ser = new XmlSerializer(typeof(XmlElement));
            TextWriter writer = new StreamWriter("sex.nsd");
            var sas = new List<VariableDeclaration>() { new VariableDeclaration ("greiner", new Type(Type.TypeOf<int>())), new VariableDeclaration("greinder", new Type(Type.TypeOf<int>())), new VariableDeclaration("weedfarm", new Type(Type.TypeOf<int>())) };
            var a = new XmlRoot("hallo", "nick", sas.ToImmutableList());
            //a.AddXmlObject(new VariableDeclaration("aaa", new Type(Type.TypeOf<int>())));
            a.AddXmlObject(new VariableSet("aaa", "4538435"));

            ser.Serialize(writer, a.Xml());
            writer.Close();
        }
    }
}

using GreinerStruct.arrz;
using GreinerStruct.XmlWriter;
using GreinerStruct.XmlWriter.Instructions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GreinerStruct
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var arrz = new ArrzFile();
            var ser = new XmlSerializer(typeof(XmlElement));
            using var writer = new StreamWriter("sex.nsd");
            //
            //var root = new XmlRoot("test", "google", new List<VariableDeclaration>().ToImmutableList(), Type.CreateType<int>(), MethodType.Sub);
            //var a = new IfElse("mag kekse");
            //a.AddXmlObject(true, new VariableDeclaration("nudel", Type.CreateType<int>()));
            //a.AddXmlObject(false, new VariableDeclaration("moritz hat eine 6 von Illerie", Type.CreateType<string>()));
            //
            //root.AddXmlObject(a);
            //root.AddXmlObject(new While("greiner < illerie").AddXmlObject(a));
            //ser.Serialize(writer, root.Xml());

            var roots = await Parser.Parse(@$"../../../../ParseTest/ParseTest.csproj");

            foreach (var root in roots)
            {
                await arrz.Add(writer, root);
            }

            await arrz.WriteArrrrrrrrrFile();
        }
    }
}

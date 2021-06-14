using GreinerStruct.arrz;
using GreinerStruct.Xml.Objects.ControlStructures.Loop;
using GreinerStruct.Xml.Objects.Inline;
using GreinerStruct.XmlWriter;
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
    internal static class Program
    {
        private static async Task Main(string[] args)
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

            //var roota = new XmlRoot("test", "google", new List<VariableDeclaration>(), new List<VariableDeclaration>(), Type.CreateType<int>(), MethodType.Sub);

            //roota.AddXmlObject(new DoWhile("Moritz == fett").AddXmlObject(new VariableDeclaration("Nudel", Type.CreateType<double>())));
            //roota.AddXmlObject(new While("Moritz == fett").AddXmlObject(new VariableDeclaration("Nudel2", Type.CreateType<double>())));
            //roota.AddXmlObject(new Endless().AddXmlObject(new VariableDeclaration("Nudel2", Type.CreateType<double>())));

            //var a = new IfElse("mag kekse");
            //a.AddXmlObject(true, new VariableDeclaration("nudel", Type.CreateType<int>()));
            //a.AddXmlObject(false, new VariableDeclaration("moritz hat eine 6 von Illerie", Type.CreateType<string>()));
            var roots = await Parser.Parse("../../../../ParseTest/ParseTest.csproj");

            ////root.AddXmlObject(a);
            ////root.AddXmlObject(new While("greiner < illerie").AddXmlObject(a));
            //ser.Serialize(writer, roota.Xml());

            foreach (var root in roots)
            {
                await arrz.Add(root);
            }

            await arrz.WriteArrrrrrrrrFile();
        }
    }
}

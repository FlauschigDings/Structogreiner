using GreinerStruct.arrz;
using System;
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
            var roots = await Parser.Parse(@$"../../../../ParseTest/ParseTest.csproj");

            await arrz.Add(writer, roots.Single());
            await arrz.Add(writer, roots.Single());

            await arrz.WriteArrrrrrrrrFile();
        }
    }
}

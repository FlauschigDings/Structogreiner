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
            var ser = new XmlSerializer(typeof(XmlElement));
            using var writer = new StreamWriter("sex.nsd");
            //var root = await Parser.Parse(@$"A:\GreinerStruct\Test\Test\Test\Test.csproj");
            var roots = await Parser.Parse(@$"C:\Users\{Environment.UserName}\source\repos\FlauschigDings\GreinerStruct\ParseTest\ParseTest.csproj");
            ser.Serialize(writer, roots.Single().Xml());
        }
    }
}

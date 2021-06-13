using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GreinerStruct
{
    class Program
    {
        static async Task Main(string[] args)
        {
            XmlSerializer ser = new XmlSerializer(typeof(XmlElement));
            TextWriter writer = new StreamWriter("sex.nsd");
            var root = await Parser.Parse(@$"A:\GreinerStruct\Test\Test\Test\Test.csproj");
            //var root = await Parser.Parse(@$"C:\Users\{Environment.UserName}\source\repos\FlauschigDings\GreinerStruct\GreinerStruct\GreinerStruct.csproj");
            ser.Serialize(writer, root.Xml());
            writer.Close();
        }
    }
}

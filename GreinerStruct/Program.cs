using System;
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
            XmlElement myElement =
            new XmlDocument().CreateElement("root", "ns");
            myElement.InnerText = "Hello World";
            TextWriter writer = new StreamWriter("sex.lsd");
            ser.Serialize(writer, new FetterSack("hallo"));
            writer.Close();

        }

        [XmlRoot("PurchaseOrder", Namespace = "http://www.cpandl.com/",
IsNullable = false)]
        public record FetterSack(string Greiner);
    }
}

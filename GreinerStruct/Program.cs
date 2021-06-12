using GreinerStruct.XmlWriter;
using GreinerStruct.XmlWriter.Instructions;
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
            TextWriter writer = new StreamWriter("sex.nsd");

            var a = new XmlRoot("hallo", "nick");
            a.AddXmlObject(new CreateVariable("aaa", "hallo"));
            a.AddXmlObject(new CreateVariable("greiner", "aaaa"));
            a.AddXmlObject(new CreateVariable("ssss", "sddd"));
            a.AddXmlObject(new CreateVariable("sdssaddsasdasss", "sddddasdsasda"));

            ser.Serialize(writer, a.Xml());
            writer.Close();
        }
    }
}

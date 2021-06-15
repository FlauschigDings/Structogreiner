using GreinerStruct.Arrz;
using GreinerStruct.I18n;
using GreinerStruct.I18n.Language;
using GreinerStruct.Xml.Objects.ControlStructures.Loops;
using GreinerStruct.Xml.Objects.Inline;
using GreinerStruct.Xml;
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

        public static II18n i18n;

        private static async Task Main(string[] args)
        {
            i18n = new GreinerLanguage();
            using var arrz = new ArrzFile();
            var ser = new XmlSerializer(typeof(XmlElement));
            using var writer = new StreamWriter("sex.nsd");

            var parser = new Parser(i18n);

            var roots = await parser.Parse("../../../../GreinerStruct/GreinerStruct.csproj");

            foreach (var root in roots)
            {
                await arrz.Add(root);
            }

            await arrz.WriteArrrrrrrrrFile();
        }
    }
}

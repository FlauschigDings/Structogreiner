using Structogreiner.Xml.Objects.ControlStructures.Loops;
using Structogreiner.Xml.Objects.Inline;
using Structogreiner.Xml;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Structogreiner.Arrz;
using Structogreiner.I18n;
using Structogreiner.I18n.Language;

namespace Structogreiner
{
    internal static class Program
    {
        public static II18n I18n { get; } = new GreinerLanguage();

        private static async Task Main(string[] args)
        {
            var projectFile = args.Length >= 1 ? args[0] : PromptProjectFile();

            var parser = new Parser();
            var roots = await parser.Parse(projectFile);

            using var arrz = new ArrzFile("output.arrz");

            foreach (var root in roots)
            {
                await arrz.Add(root);
            }

            await arrz.WriteArrrrrrrrrFile();
        }

        private static string PromptProjectFile()
        {
            Console.Write("Projektdatei (csproj): ");
            return Console.ReadLine()!;
        }
    }
}

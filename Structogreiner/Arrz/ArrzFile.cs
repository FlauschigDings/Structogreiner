using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Structogreiner.Xml;

namespace Structogreiner.Arrz
{
    internal class ArrzFile : IDisposable
    {
        private readonly ZipArchive zip;
        private int xPos, row;
        private readonly StringBuilder arrrrrrrrr;
        private readonly string fileName;

        public ArrzFile(string fileName)
        {
            xPos = 0;
            row = 0;
            this.fileName = fileName;
            arrrrrrrrr = new StringBuilder();
            zip = new ZipArchive(new FileStream(fileName, FileMode.Create), ZipArchiveMode.Create);
        }

        public void Dispose() => zip.Dispose();

        public async Task Add(Function root)
        {
            var file = $"{root.Title}.nds";

            await this.AddRootFile(root);
            this.SetArrrrrrrFile(file);
        }

        private async Task AddRootFile(Function root)
        {
            var file = $"{root.Title}.nds";

            await using var openEntry = zip.CreateEntry(file).Open();
            await using var stream = new StreamWriter(openEntry);
            await stream.WriteAsync(root.XmlString());
        }
        // Greiner pirat
        public void SetArrrrrrrFile(string file)
        {
            xPos++;
            if (xPos % 5 == 0)
            {
                xPos = 1;
                row++;
            }
            var input = $"{xPos * 350 - 350},{row * 400},\"{file}\",\"method-{xPos}\",0,0";
            arrrrrrrrr.Append(input + "\n");
        }

        public async Task WriteArrrrrrrrrFile()
        {
            await using var openEntry = zip.CreateEntry($"{fileName}.arr").Open();
            await using var stream = new StreamWriter(openEntry);

            await stream.WriteAsync(arrrrrrrrr.ToString());
        }
    }
}

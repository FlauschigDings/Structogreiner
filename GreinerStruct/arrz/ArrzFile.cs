using GreinerStruct.Xml;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Arrz
{
    internal class ArrzFile : IDisposable
    {
        private const string FileName = "autocreate";

        private readonly ZipArchive zip;
        private int xPos, row;
        private readonly StringBuilder arrrrrrrrr;
        public ArrzFile()
        {
            xPos = 0;
            row = 0;
            arrrrrrrrr = new StringBuilder();
            zip = new ZipArchive(new FileStream($"./{FileName}.arrz", FileMode.Create), ZipArchiveMode.Create);
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

            using var openEntry = zip.CreateEntry(file).Open();
            using var stream = new StreamWriter(openEntry);
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
            using var openEntry = zip.CreateEntry($"{FileName}.arr").Open();
            using var stream = new StreamWriter(openEntry);

            await stream.WriteAsync(arrrrrrrrr.ToString());
        }
    }
}

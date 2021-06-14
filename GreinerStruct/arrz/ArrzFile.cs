using GreinerStruct.XmlWriter;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.arrz
{
    class ArrzFile : IDisposable
    {
        const string FileName = "autocreate";

        private readonly ZipArchive zip;
        private int Xpos, row;
        private StringBuilder arrrrrrrrr;
        public ArrzFile()
        {
            Xpos = 0;
            row = 0;
            arrrrrrrrr = new StringBuilder();
            zip = new ZipArchive(new FileStream($"./{FileName}.arrz", FileMode.Create), ZipArchiveMode.Create);
        }

        public void Dispose() => zip.Dispose();

        public async Task Add(StreamWriter writer, XmlRoot root)
        {
            var file = $"{root.title}.nds";

            await this.AddRootFile(writer, root);
            this.SetArrrrrrrFile(file);
        }

        private async Task AddRootFile(StreamWriter writer, XmlRoot root)
        {
            var file = $"{root.title}.nds";

            using var openEntry = zip.CreateEntry(file).Open();
            using var stream = new StreamWriter(openEntry);
            await stream.WriteAsync(root.XmlString());
        }
        // Greiner pirat
        public void SetArrrrrrrFile(string file)
        {
            Xpos++;
            if (Xpos % 5 == 0)
            {
                Xpos = 0;
                row++;
            }
            var input = $"{Xpos * 350- 350},{row*400},\"{file}\",\"method-{Xpos}\",0,0";
            arrrrrrrrr.Append(input+"\n");
        }

        public async Task WriteArrrrrrrrrFile()
        {
            using var openEntry = zip.CreateEntry($"{FileName}.arr").Open();
            using var stream = new StreamWriter(openEntry);

            await stream.WriteAsync(arrrrrrrrr.ToString());
        }
    }
}

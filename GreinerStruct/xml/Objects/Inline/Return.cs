using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.Inline
{
    internal class Return : Inline
    {
        public Return(string text) : base("jump", $"RückgabeWert: {text}") { }
    }
}

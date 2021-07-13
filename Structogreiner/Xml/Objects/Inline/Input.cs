using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structogreiner.Xml.Objects.Inline
{
    internal class Input : Inline
    {
        public Input(string input) : base("instruction", Program.I18n.Input(input)) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.Xml.Objects.Inline
{
    internal class Call : Inline
    {
        public Call(string methodName) : base("call", methodName) { }
    }
}

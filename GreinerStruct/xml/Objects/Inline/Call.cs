using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.xml.Objects.Inline
{
    class Call : Inline
    {
        public Call(string methodName) : base("call", $"\"{methodName}()\"") { }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct
{
    internal record Type(string Name)
    {
        public static string TypeOf<T>() => typeof(T).FullName!.Split(".")[1];
        public override string ToString() => Program.I18n.Mappings().TryGetValue(Name, out var newName) ? newName : Name;

        public static Type CreateType<T>() => new Type(typeof(T).FullName!);

    }
}

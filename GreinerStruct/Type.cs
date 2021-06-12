using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct
{

    internal record Type(string Name)
    {
        private static readonly Dictionary<string, string> dictionary = new()
        {
            { TypeOf<int>(), "GanzZahl" },
            { TypeOf<int[]>(), "GanzZahlenArray" },

            { TypeOf<float>(), "FließKommaZahl" },
            { TypeOf<float[]>(), "FließKommaZahlArray" },

            { TypeOf<double>(), "GleitKommaZahl" },
            { TypeOf<double[]>(), "GleitKommaZahlArray" },

            { TypeOf<decimal>(), "" },
            { TypeOf<decimal[]>(), "" },

            { TypeOf<byte>(), "" },
            { TypeOf<byte[]>(), "" },

            { TypeOf<sbyte>(), "" },
            { TypeOf<sbyte[]>(), "" },

            { TypeOf<nuint>(), "" },
            { TypeOf<nuint[]>(), "" },

            { TypeOf<nint>(), "" },
            { TypeOf<nint[]>(), "" },

            //{ TypeOf<dynamic>(), "" },
            //{ TypeOf<dynamic[]>(), "" },

            { TypeOf<short>(), "" },
            { TypeOf<short[]>(), "" },

            { TypeOf<object>(), "" },
            { TypeOf<object[]>(), "" },

            { TypeOf<long>(), "GleitZahl" },
            { TypeOf<long[]>(), "GleitZahlArray" },

            { TypeOf<string>(), "ZeichenKette" },
            { TypeOf<string[]>(), "ZeichenKettenArray" },

            { TypeOf<char>(), "Zeichen" },
            { TypeOf<char[]>(), "ZeichenArray" },

            { TypeOf<bool>(), "Wahr/Falsch-Wert" },
            { TypeOf<bool[]>(), "Wahr/Falsch-WertArray" },

        };

        public static string TypeOf<T>() => typeof(T).FullName;
        public override string ToString() => dictionary.TryGetValue(Name, out var newName)? newName : Name;
        }
}

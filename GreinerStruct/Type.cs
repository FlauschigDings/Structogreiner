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

            { TypeOf<long>(), "GleitZahl" },
            { TypeOf<long[]>(), "GleitZahlArray" },

            { TypeOf<string>(), "ZeichenKette" },
            { TypeOf<string[]>(), "ZeichenKettenArray" },

            { TypeOf<char>(), "Zeichen" },
            { TypeOf<char[]>(), "ZeichenArray" },

            { TypeOf<bool>(), "Wahr/Falsch-Wert" },
            { TypeOf<bool[]>(), "Wahr/Falsch-WertArray" },

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

            { TypeOf<short>(), "" },
            { TypeOf<short[]>(), "" },

            { TypeOf<object>(), "" },
            { TypeOf<object[]>(), "" },

        };

        public static string TypeOf<T>() => typeof(T).FullName.Split(".")[1];
        public override string ToString() => dictionary.TryGetValue(Name, out var newName) ? newName : Name;

        public static Type CreateType<T>() => new Type(typeof(T).FullName);

        }
}

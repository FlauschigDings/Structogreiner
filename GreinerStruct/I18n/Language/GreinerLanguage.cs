using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.I18n.Language
{
    internal class GreinerLanguage : II18n
    {
        public string Language() => "GreinerLang";

        public string Input() => "EINGABE DIALOG";

        public string Output() => "AUSGABE DIALOG";

        public string Return() => "RückgabeWert";

        public string InputParameter() => "Eingabe Parameter";

        public string VariableDeclaration() => "Variable Deklarationen";

        public string ReturnValue() => "Rückgabe Parameter";

        public string ForStartValue() => "AW";

        public string ForEndValue() => "EW";

        public string ForStepConst() => "SW";

        public Dictionary<string, string> Mappings() => new()
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

            { TypeOf<decimal>(), "GleitkommaZahl" },
            { TypeOf<decimal[]>(), "GleitkommaZahlrray" },

            { TypeOf<byte>(), "GanzZahl" },
            { TypeOf<byte[]>(), "GanzZahlenArray" },

            { TypeOf<sbyte>(), "GanzZahl" },
            { TypeOf<sbyte[]>(), "GanzZahlenArray" },

            { TypeOf<nuint>(), "GanzZahl" },
            { TypeOf<nuint[]>(), "GanzZahlenArray" },

            { TypeOf<nint>(), "GanzZahl" },
            { TypeOf<nint[]>(), "GanzZahlenArray" },

            { TypeOf<short>(), "GanzZahl" },
            { TypeOf<short[]>(), "GanzZahlenArray" },

            { TypeOf<object>(), "Objekt" },
            { TypeOf<object[]>(), "ObjektArray" },
        };

        private static string TypeOf<T>() => typeof(T).FullName!.Split(".")[1];
    }
}

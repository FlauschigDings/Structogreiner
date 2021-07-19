using System.Collections.Generic;

namespace Structogreiner.I18n.Language
{
       // Methode: vor den Methoden name.
       // Ref und out werden mit (ref) oder (out) in variablen geschrieben. {@bsp. "test: Ganzzahl (Referenzparameter Typ out|ref)"}
       // void = keinen datentyp
       // % = in mod umbennen
       // mehrere Parameter in einer zeile, mit selben daten typ {@bsp. "stunden, minuten, sekunden: Ganzzahl"}
    internal class GreinerLanguage : II18n
    {
        public string Language() => "GreinerLang";

        public string Input(string input) => $"EINGABE DIALOG: {input}";

        public string Output(string output) => $"AUSGABE DIALOG: {output}";

        public string Return(string text) => $"Rückgabe: {text}";

        // Einrücken um 3 spaces
        public string InputParameter() => "Übergabeparameter";

        // Rein in den Body
        public string VariableDeclaration() => "Variable Deklarationen";

        public string ReturnValue() => "Rückgabedatentyp";

        public string If(string ifValue) => $"{ifValue} (?)";

        public string For(
            string variableName,
            IntVariable startValue,
            IntVariable endValue,
            IntVariable stepConst
        ) => $"{variableName}: AW={startValue}, EW={endValue}, SW={stepConst}";

        public string Foreach(
            string variableName,
            string array
        ) => $"{variableName}:  AW={1}, EW={$"{array}.Length"}, SW={1}";

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
        public string TypeOf<T>() => typeof(T).FullName!.Split(".")[1];

        public string VariableAssignment(string name, string value) => $"{name} <- {value}";

        public string VariableDeclaration(string name, Type type) => $"{name}: {type}";

        public string While(string ifValue) => $"{ifValue} (?)";

        public string DoWhile(string ifValue) => this.While(ifValue);
    }
}

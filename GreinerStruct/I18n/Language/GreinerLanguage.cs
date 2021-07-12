using System.Collections.Generic;

namespace GreinerStruct.I18n.Language
{
    internal class GreinerLanguage : II18n
    {
        public string Language() => "GreinerLang";

        public string Input(string input) => $"EINGABE DIALOG: {input}";

        public string Output(string output) => $"AUSGABE DIALOG: {output}";

        public string Return(string text) => $"RückgabeWert: {text}";

        public string InputParameter() => "Eingabe Parameter";

        public string VariableDeclaration() => "Variable Deklarationen";

        public string ReturnValue() => "Rückgabe Parameter";

        public string If(string ifValue) => $"{ifValue} ?";

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
        public string TypeOf<T>() => typeof(T).FullName!.Split(".")[1];

        public string VariableAssignment(string name, string value) => $"{name} <- {value}";

        public string VariableDeclaration(string name, Type type) => $"{name}: {type}";

        public string While(string ifValue) => $"{ifValue} (?)";

        public string DoWhile(string ifValue) => this.While(ifValue);
    }
}

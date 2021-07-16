using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structogreiner.I18n
{
    internal interface II18n : II18nLanguage
    {
        // Inline
        string Input(string input);
        string Output(string output);
        string Return(string text);

        // Function Translate
        string InputParameter();
        string VariableDeclaration();
        string ReturnValue();

        // For
        string For(
            string variableName,
            IntVariable startValue,
            IntVariable endValue,
            IntVariable stepConst
            );

        string Foreach(
            string variableName,
            string array
            );

        string If(string ifValue);
        string VariableAssignment(string name, string value);
        string VariableDeclaration(string name, Type type);

        string While(string ifValue);

        string DoWhile(string ifValue);

    }

    internal interface II18nLanguage
    {
        string Language();

        Dictionary<string, string> Mappings();

    }
}

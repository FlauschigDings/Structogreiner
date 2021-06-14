using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct.I18n
{
    interface II18n : II18nLanguage
    {
        // Inline
        string Input();
        string Output();
        string Return();

        // Function Translate
        string InputParameter();
        string VariableDeclaration();
        string ReturnValue();

        // For
        string ForStartValue();
        string ForEndValue();
        string ForStepConst();

        Dictionary<string, string> Mappings();

    }

    interface II18nLanguage
    {
        string Language();
    }
}

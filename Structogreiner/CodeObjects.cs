using System.Collections.Generic;
using Structogreiner.Xml;

namespace Structogreiner
{
    internal record CodeFile(List<Function> Methods);

    internal record Project(List<CodeFile> Files);
}
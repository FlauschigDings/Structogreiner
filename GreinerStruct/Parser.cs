using GreinerStruct.XmlWriter;
using GreinerStruct.XmlWriter.xml.Instructions;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreinerStruct
{
    internal class Parser
    {
        public static async Task<XmlRoot> Parse(string projectFile)
        {
            MSBuildLocator.RegisterDefaults();
            using var workspace = MSBuildWorkspace.Create();
            var project = await workspace.OpenProjectAsync(projectFile);

            var variables = new List<VariableDeclaration>();

            foreach (var document in project.Documents)
            {
                await ParseDocument(variables, document);
            }

            return new XmlRoot(Path.GetFileName(projectFile), Environment.UserName, variables.ToImmutableList());
        }

        private static async Task ParseDocument(List<VariableDeclaration> variables, Document document)
        {
            var rootNode = (await document.GetSyntaxRootAsync())!;
            var semanticModel = (await document.GetSemanticModelAsync())!;

            foreach (var node in rootNode.DescendantNodes())
            {
                if (node is MethodDeclarationSyntax method && method.Identifier.Text == "Main")
                {
                    ParseMethod(variables, semanticModel, node);
                }
            }
        }

        private static void ParseMethod(List<VariableDeclaration> variables, SemanticModel semanticModel, SyntaxNode node)
        {
            foreach (var subNode in node.DescendantNodes())
            {
                if (subNode is VariableDeclarationSyntax vd)
                {
                    var name = vd.Variables[0].Identifier.Text;
                    var lvd = (LocalDeclarationStatementSyntax)vd.Parent!;
                    var symbolInfo = semanticModel.GetSymbolInfo(lvd.Declaration.Type);
                    var type = symbolInfo.Symbol?.Name;
                    variables.Add(new VariableDeclaration(name, new Type(type)));
                }
            }
        }
    }
}

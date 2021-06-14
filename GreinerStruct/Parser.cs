using GreinerStruct.XmlWriter;
using GreinerStruct.XmlWriter.Instructions;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace GreinerStruct
{
    internal class Parser
    {
        public static async Task<List<XmlRoot>> Parse(string projectFile)
        {
            MSBuildLocator.RegisterDefaults();
            using var workspace = MSBuildWorkspace.Create();
            var project = await workspace.OpenProjectAsync(projectFile);
            var roots = new List<XmlRoot>();

            foreach (var document in project.Documents)
            {
                roots.AddRange(await ParseDocument(document));
            }
            return roots;
        }

        private static async Task<List<XmlRoot>> ParseDocument(Document document)
        {
            var rootNode = (await document.GetSyntaxRootAsync())!;
            var semanticModel = (await document.GetSemanticModelAsync())!;

            var methods = new List<XmlRoot>();

            foreach (var node in rootNode.DescendantNodes())
            {
                if (node is MethodDeclarationSyntax method)
                {
                    methods.Add(ParseMethod(method, semanticModel));
                }
            }
            return methods;
        }

        private static XmlRoot ParseMethod(MethodDeclarationSyntax method, SemanticModel semanticModel)
        {
            var variables = new List<VariableDeclaration>();
            foreach (var node in method.DescendantNodes())
            {
                if (node is LocalDeclarationStatementSyntax lvd)
                {
                    ParseVariables(lvd, semanticModel, variables);
                }
                if (node is ForStatementSyntax fs)
                {
                    var symbolInfo = semanticModel.GetSymbolInfo(fs.Declaration.Type);
                    var varType = symbolInfo.Symbol?.Name;
                    variables.Add(new VariableDeclaration(fs.Declaration.Variables[0].Identifier.Text, new Type(varType)));
                }
            }
            var instructions = ParseBlock(method.Body);

            var type = method.Identifier.Text == "Main" ? MethodType.Main : MethodType.Sub;
            var returnType = new Type(method.ReturnType.ToString());

            var root = new XmlRoot(method.Identifier.Text, "", variables.ToImmutableList(), returnType, type);
            foreach (var instruction in instructions)
            {
                root.AddXmlObject(instruction);
            }
            return root;
        }

        private static List<XmlObject> ParseBlock(SyntaxNode block)
        {
            var instructions = new List<XmlObject>();
            foreach (var node in block.ChildNodes())
            {
                if (node is ForStatementSyntax fs)
                {
                    instructions.Add(ParseFor(fs));
                }

                if (node is ExpressionStatementSyntax expression && expression.Expression is AssignmentExpressionSyntax assignment)
                {
                    instructions.Add(ParseVariableAssignment(assignment));
                }
                if (node is LocalDeclarationStatementSyntax lvd)
                {
                    ParseVariableAssignment(lvd, instructions);
                }

                if (node is SwitchStatementSyntax ss)
                {
                    var sections = ss.Sections;
                    // new XmlSwitch(ss.Expression.ToString(), )
                }
            }
            return instructions;
        }

        private static VariableAssignment ParseVariableAssignment(AssignmentExpressionSyntax assignment)
        {
            return new VariableAssignment(assignment.Left.ToString(), assignment.Right.ToString());
        }

        private static void ParseVariableAssignment(LocalDeclarationStatementSyntax lvd, List<XmlObject> instructions)
        {
            foreach (var node in lvd.DescendantNodes())
            {
                if (node is VariableDeclaratorSyntax vd)
                {
                    var name = vd.Identifier.Text;
                    var value = vd.Initializer.Value.ToString();
                    instructions.Add(new VariableAssignment(name, value));
                }
            }
        }

        private static XmlFor ParseFor(ForStatementSyntax fs)
        {
            var forVar = fs.Declaration.Variables[0];
            var forVarName = forVar.Identifier.Text;

            var startValue = new IntVariable(forVar.Initializer.Value.ToString());
            var cond = (BinaryExpressionSyntax)fs.Condition;

            var endValue = new IntVariable(cond.Right.ToString());
            if (cond.IsKind(SyntaxKind.LessThanToken))
            {
                endValue.Subtract(1);
            }

            var incrementor = fs.Incrementors[0];
            var step = incrementor switch
            {
                PostfixUnaryExpressionSyntax => incrementor.IsKind(SyntaxKind.PostIncrementExpression) ? new IntVariable(1) : new IntVariable(-1),
                AssignmentExpressionSyntax assignment when assignment.OperatorToken.IsKind(SyntaxKind.AddAssignmentExpression) => new IntVariable(assignment.Right.ToString()),
                AssignmentExpressionSyntax assignment when assignment.OperatorToken.IsKind(SyntaxKind.SubtractAssignmentExpression) => new IntVariable(assignment.Right.ToString()).ToNegative()
            };
            var xmlFor = new XmlFor(forVarName, startValue, endValue, step);
            foreach (var instruction in ParseBlock(fs.Statement))
            {
                xmlFor.AddXmlObject(instruction);
            }
            return xmlFor;
        }

        private static void ParseVariables(LocalDeclarationStatementSyntax lvd, SemanticModel semanticModel, List<VariableDeclaration> variables)
        {
            var symbolInfo = semanticModel.GetSymbolInfo(lvd.Declaration.Type);
            var type = symbolInfo.Symbol?.Name;

            foreach (var node in lvd.DescendantNodes())
            {
                if (node is VariableDeclarationSyntax vd)
                {
                    var name = vd.Variables[0].Identifier.Text;
                    variables.Add(new VariableDeclaration(name, new Type(type)));
                }
            }
        }
    }
}

using GreinerStruct.Xml.Objects.ControlStructures;
using GreinerStruct.Xml.Objects.ControlStructures.Loops;
using GreinerStruct.Xml.Objects.Inline;
using GreinerStruct.XmlWriter;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreinerStruct
{
    internal class Parser
    {
        public static async Task<List<Function>> Parse(string projectFile)
        {
            MSBuildLocator.RegisterDefaults();
            using var workspace = MSBuildWorkspace.Create();
            var project = await workspace.OpenProjectAsync(projectFile);
            var roots = new List<Function>();

            foreach (var document in project.Documents)
            {
                roots.AddRange(await ParseDocument(document));
            }
            return roots;
        }

        private static async Task<List<Function>> ParseDocument(Document document)
        {
            var rootNode = (await document.GetSyntaxRootAsync())!;
            var semanticModel = (await document.GetSemanticModelAsync())!;

            var methods = new List<Function>();

            foreach (var node in rootNode.DescendantNodes())
            {
                if (node is MethodDeclarationSyntax method)
                {
                    ParseMethod(method, semanticModel, methods);
                }
            }
            return methods;
        }

        private static void ParseMethod(MethodDeclarationSyntax method, SemanticModel semanticModel, List<Function> methods)
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

            var parameters = method.ParameterList.Parameters.Select(p => new VariableDeclaration(p.Identifier.Text, new Type(semanticModel.GetSymbolInfo(p.Type).Symbol.Name))).ToList();

            var objects = ParseBlock(method.Body);

            var type = method.Identifier.Text == "Main" ? MethodType.Main : MethodType.Sub;
            var returnType = new Type(method.ReturnType.ToString());

            var root = new Function(method.Identifier.Text, "", variables, parameters, returnType, type);
            foreach (var instruction in objects)
            {
                root.AddXmlObject(instruction);
            }
            methods.Add(root);
        }

        private static List<XmlObject> ParseBlock(SyntaxNode block)
        {
            var objects = new List<XmlObject>();
            foreach (var node in block.ChildNodes())
            {
                if (node is ForStatementSyntax fs)
                {
                    ParseFor(fs, objects);
                }

                if (node is ExpressionStatementSyntax expression && expression.Expression is AssignmentExpressionSyntax assignment)
                {
                    ParseVariableAssignment(assignment, objects);
                }
                if (node is LocalDeclarationStatementSyntax lvd)
                {
                    ParseVariableAssignment(lvd, objects);
                }

                if (node is IfStatementSyntax ifs)
                {
                    ParseIfElse(ifs, objects);
                }
                if (node is ReturnStatementSyntax rs)
                {
                    ParseReturn(rs, objects);
                }
                if (node is WhileStatementSyntax wh)
                {
                    ParseWhile(wh, objects);
                }
                if (node is DoStatementSyntax ds)
                {
                    ParseWhileDo(ds, objects);
                }
                if (node is ExpressionStatementSyntax expression1 && expression1.Expression is InvocationExpressionSyntax invocation)
                {
                    ParseMethods(invocation, objects);
                }
                if (node is SwitchStatementSyntax sw)
                {
                    ParseSwitch(sw, objects);
                }
            }
            return objects;
        }

        private static void ParseSwitch(SwitchStatementSyntax sw, List<XmlObject> objects)
        {
            Console.WriteLine(sw.Expression.ToString());
            Console.WriteLine();

            var list = new List<string>();
            var lisst = new List<string>();

            foreach (var a in sw.Sections)
            {
                var casE = a.Labels.ToString();
                if (casE.Contains("case"))
                {
                    int from = casE.IndexOf("\"");
                    int to = casE.LastIndexOf("\"");
                    casE = casE.Substring(from + 1, to - from - 1);
                    list.Add(casE);
                    continue;
                }
                casE = casE.Substring(0, casE.Length - 1);
                list.Add(casE);
            }
            var switchState = new Switch(sw.Expression.ToString(), list.ToArray());
            for (var i = 0; i < sw.Sections.Count; i++)
            {
                ParseBlock(sw.Sections[i]).ForEach(e => switchState.AddXmlObject(i, e));
            }
            objects.Add(switchState);
        }

        private static void ParseMethods(InvocationExpressionSyntax invocation, List<XmlObject> objects)
        {
            if (invocation.Expression.ToString() == "Console.WriteLine")
            {
                var str = invocation.ToString();

                int from = str.IndexOf("(");
                int to = str.LastIndexOf(")");
                str = str.Substring(from + 1, to - from - 1);
                objects.Add(new Output(str));
                return;
            }
            var name = invocation.ToString();

            // Rlly nice crystals
            var meth = new Call(name);
            objects.Add(meth);
        }

        private static void ParseWhileDo(DoStatementSyntax ds, List<XmlObject> objects)
        {
            var whileVar = new DoWhile(ds.Condition.ToString());
            ParseBlock(ds.Statement).ForEach(e => whileVar.AddXmlObject(e));
            objects.Add(whileVar);
        }

        private static void ParseWhile(WhileStatementSyntax wh, List<XmlObject> objects)
        {
            // While true
            if (wh.Condition.ToString() == "true")
            {
                var endless = new Endless();
                ParseBlock(wh.Statement).ForEach(e => endless.AddXmlObject(e));
                objects.Add(endless);
                return;
            }
            var whileVar = new While(wh.Condition.ToString());
            ParseBlock(wh.Statement).ForEach(e => whileVar.AddXmlObject(e));
            objects.Add(whileVar);
        }

        private static void ParseIfElse(IfStatementSyntax ifs, List<XmlObject> objects)
        {
            var ifElse = new IfElse(ifs.Condition.ToString());
            foreach (var xmlObj in ParseBlock(ifs.Statement))
            {
                ifElse.AddXmlObject(true, xmlObj);
            }
            if (ifs.Else is not null)
            {
                foreach (var xmlObj in ParseBlock(ifs.Else))
                {
                    ifElse.AddXmlObject(true, xmlObj);
                }
            }
            objects.Add(ifElse);
        }

        private static void ParseReturn(ReturnStatementSyntax rs, List<XmlObject> objects)
        {
            objects.Add(new Return(rs.Expression.ToString()));
        }

        private static void ParseVariableAssignment(AssignmentExpressionSyntax assignment, List<XmlObject> objects)
        {
            objects.Add(new VariableAssignment(assignment.Left.ToString(), assignment.Right.ToString()));
        }

        private static void ParseVariableAssignment(LocalDeclarationStatementSyntax lvd, List<XmlObject> objects)
        {
            foreach (var node in lvd.DescendantNodes())
            {
                if (node is VariableDeclaratorSyntax vd)
                {
                    var name = vd.Identifier.Text;
                    var value = vd.Initializer.Value.ToString();
                    objects.Add(new VariableAssignment(name, value));
                }
            }
        }

        private static void ParseFor(ForStatementSyntax fs, List<XmlObject> objects)
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
            var xmlFor = new For(forVarName, startValue, endValue, step);
            foreach (var xmlObj in ParseBlock(fs.Statement))
            {
                xmlFor.AddXmlObject(xmlObj);
            }
            objects.Add(xmlFor);
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

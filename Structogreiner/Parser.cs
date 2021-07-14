using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Structogreiner.Xml;
using Structogreiner.Xml.Objects.ControlStructures;
using Structogreiner.Xml.Objects.ControlStructures.Loops;
using Structogreiner.Xml.Objects.Inline;

namespace Structogreiner
{
    internal class Parser
    {
        public async Task<List<Function>> Parse(string projectFile)
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

        private async Task<List<Function>> ParseDocument(Document document)
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

        private void ParseMethod(MethodDeclarationSyntax method, SemanticModel semanticModel, List<Function> methods)
        {
            var variables = new List<VariableDeclaration>();
            foreach (var node in method.DescendantNodes())
            {
                switch (node)
                {
                    case LocalDeclarationStatementSyntax lvd:
                        ParseVariables(lvd, semanticModel, variables);
                        break;
                    case ForStatementSyntax fs:
                        {
                            var declaration = fs.Declaration;
                            if (declaration is null) break;
                            var symbolInfo = semanticModel.GetSymbolInfo(declaration.Type);
                            var varType = symbolInfo.Symbol?.Name!;
                            variables.Add(new VariableDeclaration(declaration.Variables[0].Identifier.Text,
                                new Type(varType)));
                            break;
                        }
                }
            }

            var parameters = method.ParameterList.Parameters.Select(p =>
            {
                var symbolInfo = semanticModel.GetSymbolInfo(p.Type!);
                var parmType = new Type(GetSymbolName(symbolInfo));
                if (symbolInfo.Symbol!.Kind == SymbolKind.ArrayType)
                {
                    parmType.IsArray = true;
                }
                return new VariableDeclaration(p.Identifier.Text, parmType);
            }).ToList();

            var block = (SyntaxNode?)method.Body ?? method.ExpressionBody;
            if (block is null) return;

            var objects = ParseNode(block);

            var type = method.Identifier.Text == "Main" ? MethodType.Main : MethodType.Sub;
            var returnType = new Type(method.ReturnType.ToString());

            var root = new Function(method.Identifier.Text, "", variables, parameters, returnType, type);
            foreach (var instruction in objects)
            {
                root.AddXmlObject(instruction);
            }

            methods.Add(root);
        }

        private void ParseBlock(SyntaxNode block, List<XmlObject> objects)
        {
            foreach (var node in block.ChildNodes())
            {
                objects.AddRange(ParseNode(node));
            }
        }

        private List<XmlObject> ParseNode(SyntaxNode node)
        {
            var objects = new List<XmlObject>();

            if (node is ExpressionStatementSyntax expression)
            {
                if (expression.Expression is AssignmentExpressionSyntax assignment)
                {
                    ParseVariableAssignment(assignment, objects);
                }
                else if (expression.Expression is InvocationExpressionSyntax invocation)
                {
                    ParseMethods(invocation, objects);
                }
                else
                {
                    objects.Add(new Input(expression.Expression.ToString()));
                }
            }

            if (node is ForStatementSyntax fs)
            {
                ParseFor(fs, objects);
            }

            if (node is DoStatementSyntax ds)
            {
                ParseWhileDo(ds, objects);
            }

            if (node is WhileStatementSyntax wh)
            {
                ParseWhile(wh, objects);
            }

            if (node is IfStatementSyntax ifs)
            {
                ParseIfElse(ifs, objects);
            }

            if (node is SwitchStatementSyntax sw)
            {
                ParseSwitch(sw, objects);
            }

            if (node is LocalDeclarationStatementSyntax lvd)
            {
                ParseVariableAssignment(lvd, objects);
            }

            if (node is ReturnStatementSyntax rs)
            {
                ParseReturn(rs, objects);
            }

            if (node is ForEachStatementSyntax es)
            {
                ParseForeach(es, objects);
            }

            if (node is BlockSyntax block)
            {
                ParseBlock(block, objects);
            }

            return objects;
        }

        private void ParseForeach(ForEachStatementSyntax es, List<XmlObject> objects)
        {
            //Console.WriteLine(es.Statement);
            //Console.WriteLine(es.Identifier.ToString());
            //Console.WriteLine(es.Expression.ToString());

            //var Foreach = new For(es.Identifier.ToString(), 0, , 1); 

            var xmlFor = new Foreach(es.Identifier.ToString(), es.Expression.ToString());
            ParseNode(es.Statement).ForEach(e =>
            {
                Console.WriteLine(e.XmlString());
                xmlFor.AddXmlObject(e);
            });
            objects.Add(xmlFor);
        }

        private void ParseSwitch(SwitchStatementSyntax sw, List<XmlObject> objects)
        {
            var xmlLabels = sw.Sections.Select(section => string.Join(", ",
                section.Labels.Select(label =>
                {
                    var labelStr =
                        ((label as CasePatternSwitchLabelSyntax)?.Pattern.ToString() ??
                         (label as CaseSwitchLabelSyntax)?.Value.ToString()) ?? "default";
                    return labelStr.Replace("\"", "\"\"");
                }))
            ).ToArray();
            var switchState = new Switch(sw.Expression.ToString(), xmlLabels);
            for (var i = 0; i < sw.Sections.Count; i++)
            {
                foreach (var xmlObj in ParseNode(sw.Sections[i]))
                {
                    switchState.AddXmlObject(i, xmlObj);
                }
            }

            objects.Add(switchState);
        }

        private void ParseMethods(InvocationExpressionSyntax invocation, ICollection<XmlObject> objects)
        {
            if (invocation.Expression.ToString() == "Console.WriteLine")
            {
                var str = invocation.ToString();

                var from = str.IndexOf("(", StringComparison.Ordinal);
                var to = str.LastIndexOf(")", StringComparison.Ordinal);
                str = str.Substring(from + 1, to - from - 1);
                objects.Add(new Output(str));
                return;
            }

            var name = invocation.ToString();

            // Rlly nice crystals
            var meth = new Call(name);
            objects.Add(meth);
        }

        private void ParseWhileDo(DoStatementSyntax ds, ICollection<XmlObject> objects)
        {
            var whileVar = new DoWhile(ds.Condition.ToString());
            ParseNode(ds.Statement).ForEach(e => whileVar.AddXmlObject(e));
            objects.Add(whileVar);
        }

        private void ParseWhile(WhileStatementSyntax wh, List<XmlObject> objects)
        {
            // While true
            if (wh.Condition.ToString() == "true")
            {
                var endless = new Endless();
                ParseNode(wh.Statement).ForEach(e => endless.AddXmlObject(e));
                objects.Add(endless);
                return;
            }

            var whileVar = new While(wh.Condition.ToString());
            ParseNode(wh.Statement).ForEach(e => whileVar.AddXmlObject(e));
            objects.Add(whileVar);
        }

        private void ParseIfElse(IfStatementSyntax ifs, List<XmlObject> objects)
        {
            var ifElse = new IfElse(ifs.Condition.ToString());
            foreach (var xmlObj in ParseNode(ifs.Statement))
            {
                ifElse.AddXmlObject(true, xmlObj);
            }

            if (ifs.Else is not null)
            {
                foreach (var xmlObj in ParseNode(ifs.Else))
                {
                    ifElse.AddXmlObject(true, xmlObj);
                }
            }

            objects.Add(ifElse);
        }

        private void ParseReturn(ReturnStatementSyntax rs, List<XmlObject> objects)
        {
            objects.Add(new Return(rs.Expression?.ToString() ?? ""));
        }

        private void ParseVariableAssignment(AssignmentExpressionSyntax assignment, List<XmlObject> objects)
        {
            objects.Add(new VariableAssignment(assignment.Left.ToString(), assignment.Right.ToString()));
        }

        private void ParseVariableAssignment(LocalDeclarationStatementSyntax lvd, ICollection<XmlObject> objects)
        {
            foreach (var node in lvd.DescendantNodes())
            {
                if (node is not VariableDeclaratorSyntax vd) continue;
                var name = vd.Identifier.Text;
                if (vd.Initializer == null) continue;
                var value = vd.Initializer.Value.ToString();
                if (value == "Console.ReadKey()")
                {
                    objects.Add(new Input(name));
                    continue;
                }

                objects.Add(new VariableAssignment(name, value));
            }
        }

        private void ParseFor(ForStatementSyntax fs, List<XmlObject> objects)
        {
            if (fs.Declaration is null) return;
            var forVar = fs.Declaration.Variables[0];
            if (forVar.Initializer is null) return;

            var forVarName = forVar.Identifier.Text;

            var startValue = new IntVariable(forVar.Initializer.Value.ToString());
            var cond = (BinaryExpressionSyntax?)fs.Condition;
            if (cond is null) return;

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
                AssignmentExpressionSyntax assignment when assignment.OperatorToken.IsKind(SyntaxKind.SubtractAssignmentExpression) => new IntVariable(assignment.Right.ToString()).ToNegative(),
                _ => throw new InvalidOperationException("Unsupported incrementor.")
            };
            var xmlFor = new For(forVarName, startValue, endValue, step);
            foreach (var xmlObj in ParseNode(fs.Statement))
            {
                xmlFor.AddXmlObject(xmlObj);
            }

            objects.Add(xmlFor);
        }

        private void ParseVariables(LocalDeclarationStatementSyntax lvd, SemanticModel semanticModel,
            ICollection<VariableDeclaration> variables)
        {
            var symbolInfo = semanticModel.GetSymbolInfo(lvd.Declaration.Type);
            var type = GetSymbolName(symbolInfo);
            var typeObj = new Type(type);
            if (symbolInfo.Symbol!.Kind == SymbolKind.ArrayType)
            {
                typeObj.IsArray = true;
            }

            Console.WriteLine(type);

            foreach (var node in lvd.DescendantNodes())
            {
                if (node is not VariableDeclarationSyntax vd) continue;

                var name = vd.Variables[0].Identifier.Text;
                variables.Add(new VariableDeclaration(name, typeObj));
            }
        }

        private string GetSymbolName(SymbolInfo symbolInfo)
        {
            var type = symbolInfo.Symbol!.Name;
            if (symbolInfo.Symbol is IArrayTypeSymbol arraySymbol)
            {
                type = arraySymbol.ElementType.Name;
            }
            return type;
        }

        public static string TypeOf<T>() => typeof(T).FullName!.Split(".")[1];

    }
}
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
        private SemanticModel? _semanticModel;

        public async Task<Project> Parse(string projectFile)
        {
            MSBuildLocator.RegisterDefaults();
            using var workspace = MSBuildWorkspace.Create();

            var project = await workspace.OpenProjectAsync(projectFile);
            var codeFiles = new List<CodeFile>();

            foreach (var document in project.Documents)
            {
                codeFiles.Add(await ParseDocument(document));
            }

            return new Project(codeFiles);
        }

        private async Task<CodeFile> ParseDocument(Document document)
        {
            var rootNodeTask = document.GetSyntaxRootAsync();
            var semanticModelTask = document.GetSemanticModelAsync();

            var rootNode = (await rootNodeTask)!;
            _semanticModel = (await semanticModelTask)!;

            var methods = new List<Function>();

            foreach (var node in rootNode.DescendantNodes())
            {
                if (node is MethodDeclarationSyntax statement)
                {
                }
            }

            return new CodeFile(methods);
        }

        private Function ParseMethod(MethodDeclarationSyntax method)
        {
            var variables = new List<VariableDeclaration>();
            foreach (var node in method.DescendantNodes())
            {
                switch (node)
                {
                    case LocalDeclarationStatementSyntax statement:
                        foreach (var variable in statement.Declaration.Variables)
                        {
                            variables.Add(new VariableDeclaration(variable.Identifier.Text,
                                GetTypeFromSymbol(statement.Declaration.Type)!));
                        }
                        break;

                    case ForStatementSyntax statement:
                        var declaration = statement.Declaration;
                        if (declaration is null) break;
                        variables.Add(new VariableDeclaration(declaration.Variables[0].Identifier.Text, GetTypeFromSymbol(declaration.Type)));
                        break;
                }
            }

            var parameters = method.ParameterList.Parameters
                .Select(p => new VariableDeclaration(p.Identifier.Text, GetTypeFromSymbol(p.Type!))).ToList();

            var methodNode = (SyntaxNode?)method.Body ?? method.ExpressionBody!;

            var type = method.Identifier.Text == "Main" ? MethodType.Main : MethodType.Sub;
            var returnType = new Type(method.ReturnType.ToString());
            var block = ParseBlock(methodNode);

            return new Function(method.Identifier.ToString(), "", variables, parameters, returnType, type);
            //if (block is null) return;

            //var objects = ParseNode(block);

            //var root = new Function(method.Identifier.Text, "", variables, parameters, returnType, type);
            //foreach (var instruction in objects)
            //{
            //    root.AddXmlObject(instruction);
            //}
            //
            //methods.Add(root);
        }

        private Block ParseBlock(SyntaxNode blockNode)
        {
            var instructions = new List<IInstruction>();
            foreach (var node in blockNode.ChildNodes())
            {
                IInstruction? instruction = node switch
                {
                    BlockSyntax block => ParseBlock(block),
                    IfStatementSyntax ifStatement => ParseIfStatement(ifStatement),
                    _ => null
                };
                if (instruction is not null)
                {
                    instructions.Add(instruction);
                }
            }
            return new Block(instructions);
        }

        private IExpression ParseExpression(ExpressionSyntax expressionNode)
        {
            return expressionNode switch
            {
                BinaryExpressionSyntax binaryExpressionNode => ParseBinaryExpression(binaryExpressionNode),
                _ => null!
            };
        }

        private BinaryExpression ParseBinaryExpression(BinaryExpressionSyntax binaryExpressionNode)
        {
            Operator? exprOperator = binaryExpressionNode.OperatorToken.Kind() switch
            {
                SyntaxKind.LogicalAndExpression => Operator.LogicalAnd,
                SyntaxKind.BitwiseAndExpression => Operator.BitwiseAnd,
                SyntaxKind.LogicalOrExpression => Operator.LogicalAnd,
                SyntaxKind.BitwiseOrExpression => Operator.BitwiseOr,
                SyntaxKind.LogicalNotExpression => Operator.LogicalNot,
                SyntaxKind.AddExpression => Operator.Add,
                SyntaxKind.SubtractExpression => Operator.Subtract,
                SyntaxKind.MultiplyExpression => Operator.Multiply,
                SyntaxKind.DivideExpression => Operator.Divide,
                SyntaxKind.ModuloExpression => Operator.Modulo,
                _ => null
            };
            return new BinaryExpression((Operator)exprOperator!, ParseExpression(binaryExpressionNode.Left), ParseExpression(binaryExpressionNode.Right));
        }

        private IfStatement ParseIfStatement(IfStatementSyntax ifStatementNode)
        {
            return new IfStatement(ParseExpression(ifStatementNode.Condition)!, ParseBlock(ifStatementNode.Statement));
        }

        private Type GetTypeFromSymbol(TypeSyntax type) => new(_semanticModel.GetSymbolInfo(type).Symbol?.ToString()!);
    }
}
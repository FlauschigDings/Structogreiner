using System.Collections.Generic;
using Structogreiner.Xml;

namespace Structogreiner
{
    internal interface IInstruction
    {

    }

    internal interface IExpression : IInstruction
    {

    }

    internal enum Operator
    {
        LogicalAnd,
        BitwiseAnd,
        LogicalOr,
        BitwiseOr,
        LogicalNot,
        Add,
        Subtract,
        Multiply,
        Divide,
        Modulo
    }

    internal record CodeFile(List<Function> Methods);

    internal record Project(List<CodeFile> Files);

    internal record Block(List<IInstruction> Instructions) : IInstruction;

    internal record BinaryExpression(Operator Operator, IExpression Left, IExpression Right) : IExpression;

    internal record IfStatement(IExpression Condition, Block Statement) : IInstruction;
}
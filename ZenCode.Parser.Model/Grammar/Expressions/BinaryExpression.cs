using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record BinaryExpression : Expression
{
    public required Token Operator { get; init; }
    public required Expression LeftOperand { get; init; }
    public required Expression RightOperand { get; init; }

    public override string ToString()
    {
        return $"{LeftOperand} {Operator} {RightOperand}";
    }
}

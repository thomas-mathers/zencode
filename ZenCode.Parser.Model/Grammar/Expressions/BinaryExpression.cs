namespace ZenCode.Parser.Model.Grammar.Expressions;

public record BinaryExpression : Expression
{
    public required BinaryOperatorType Operator { get; init; }
    public required Expression Left { get; init; }
    public required Expression Right { get; init; }

    public override string ToString()
    {
        return $"{Left} {Operator} {Right}";
    }
}

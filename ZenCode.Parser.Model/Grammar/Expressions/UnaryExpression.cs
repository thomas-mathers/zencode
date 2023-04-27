namespace ZenCode.Parser.Model.Grammar.Expressions;

public record UnaryExpression : Expression
{
    public required UnaryOperatorType Operator { get; init; }
    public required Expression Expression { get; init; }

    public override string ToString()
    {
        return $"{Operator} {Expression}";
    }
}

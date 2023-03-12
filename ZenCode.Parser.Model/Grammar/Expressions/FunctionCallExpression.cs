namespace ZenCode.Parser.Model.Grammar.Expressions;

public record FunctionCallExpression(Expression Expression) : Expression
{
    public ExpressionList Arguments { get; init; } = new();
}
namespace ZenCode.Parser.Model.Grammar.Expressions;

public record FunctionCallExpression(VariableReferenceExpression VariableReferenceExpression) : Expression
{
    public ExpressionList Arguments { get; init; } = new();
}
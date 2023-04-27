namespace ZenCode.Parser.Model.Grammar.Expressions;

public record UnaryExpression(UnaryOperatorType Operator, Expression Expression) : Expression
{
    public override string ToString()
    {
        return $"{Operator} {Expression}";
    }
}

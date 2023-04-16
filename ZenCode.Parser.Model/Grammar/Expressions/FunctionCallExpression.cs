using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record FunctionCallExpression(Expression Expression) : Expression
{
    public Token LeftParenthesis { get; init; }
    public ExpressionList Arguments { get; init; } = new();

    public override string ToString()
    {
        return $"{Expression}({Arguments})";
    }

    public virtual bool Equals(FunctionCallExpression? other)
    {
        return Expression.Equals(other.Expression) && Arguments.Equals(other.Arguments);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Arguments, Expression);
    }
}

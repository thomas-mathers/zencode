using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record FunctionCallExpression : Expression
{
    public required Expression FunctionReference { get; init; }
    public ExpressionList Arguments { get; init; } = new();

    public override string ToString()
    {
        return $"{FunctionReference}({Arguments})";
    }

    public virtual bool Equals(FunctionCallExpression? other)
    {
        return FunctionReference.Equals(other.FunctionReference) && Arguments.Equals(other.Arguments);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Arguments, FunctionReference);
    }
}

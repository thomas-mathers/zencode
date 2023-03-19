using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar;

public record ExpressionList : AstNode
{
    public IReadOnlyList<Expression> Expressions { get; init; } = Array.Empty<Expression>();

    public virtual bool Equals(ExpressionList? other)
    {
        return other != null && Expressions.SequenceEqual(other.Expressions);
    }

    public override int GetHashCode()
    {
        return Expressions.GetHashCode();
    }
    
    public override string ToString()
    {
        return string.Join(", ", Expressions);
    }
}
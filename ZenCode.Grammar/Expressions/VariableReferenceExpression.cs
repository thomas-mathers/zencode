using ZenCode.Lexer.Model;

namespace ZenCode.Grammar.Expressions;

public record VariableReferenceExpression(Token Identifier, IReadOnlyList<Expression> Indices) : Expression
{
    public virtual bool Equals(VariableReferenceExpression? other)
    {
        return other != null && Identifier.Equals(other.Identifier) && Indices.SequenceEqual(other.Indices);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Identifier, Indices);
    }
}

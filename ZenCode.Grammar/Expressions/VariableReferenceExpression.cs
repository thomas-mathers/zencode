using ZenCode.Lexer.Model;

namespace ZenCode.Grammar.Expressions;

public record VariableReferenceExpression : Expression
{
    public required Token Identifier { get; init; }
    public IReadOnlyList<Expression> Indices { get; init; } = Array.Empty<Expression>();
    
    public virtual bool Equals(VariableReferenceExpression? other)
    {
        return other != null && Identifier.Equals(other.Identifier) && Indices.SequenceEqual(other.Indices);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Identifier, Indices);
    }
}
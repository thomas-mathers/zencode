namespace ZenCode.Parser.Model.Grammar.Statements;

public record IfStatement(ConditionScope ThenScope) : CompoundStatement
{
    public IReadOnlyList<ConditionScope> ElseIfScopes { get; init; } = Array.Empty<ConditionScope>();
    public Scope? ElseScope { get; init; }

    public virtual bool Equals(IfStatement? other)
    {
        return other != null
               && ThenScope.Equals(other.ThenScope)
               && ElseIfScopes.SequenceEqual(other.ElseIfScopes)
               && Equals(ElseScope, other.ElseScope);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ElseIfScopes, ElseScope, ThenScope);
    }
}
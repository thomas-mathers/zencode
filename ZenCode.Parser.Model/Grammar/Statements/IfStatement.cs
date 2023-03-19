using System.Text;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record IfStatement(ConditionScope ThenScope) : CompoundStatement
{
    public IReadOnlyList<ConditionScope> ElseIfScopes { get; init; } = Array.Empty<ConditionScope>();
    public Scope? ElseScope { get; init; }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append(ThenScope);

        foreach (var scope in ElseIfScopes)
        {
            stringBuilder.Append(' ');
            stringBuilder.Append(scope);
        }

        if (ElseScope != null)
        {
            stringBuilder.Append(' ');
            stringBuilder.Append(ElseScope);
        }

        return stringBuilder.ToString();
    }

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
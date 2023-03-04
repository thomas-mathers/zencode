using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Model;

public record Scope
{
    public IReadOnlyList<Statement> Statements { get; init; } = Array.Empty<Statement>();

    public virtual bool Equals(Scope? other)
    {
        return other != null && Statements.SequenceEqual(other.Statements);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Statements);
    }
}
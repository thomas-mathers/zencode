namespace ZenCode.Parser.Model.Grammar.Statements;

public record Scope : Statement
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
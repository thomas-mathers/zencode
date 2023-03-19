using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Model.Grammar;

public record Scope : AstNode
{
    public IReadOnlyList<Statement> Statements { get; init; } = Array.Empty<Statement>();
    
    public virtual bool Equals(Scope? other)
    {
        return other != null && Statements.SequenceEqual(other.Statements);
    }

    public override int GetHashCode()
    {
        return Statements.GetHashCode();
    }
    
    public override string ToString()
    {
        return $"{{ {string.Join('\n', Statements)} }}";
    }
}
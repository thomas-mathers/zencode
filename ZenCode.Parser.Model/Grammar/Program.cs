using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Model.Grammar;

public record Program(IReadOnlyList<Statement> Statements) : AstNode
{
    public virtual bool Equals(Program? other)
    {
        return other != null && Statements.SequenceEqual(other.Statements);
    }

    public override int GetHashCode()
    {
        return Statements.GetHashCode();
    }
    
    public override string ToString()
    {
        return string.Join("\n", Statements);
    }
}
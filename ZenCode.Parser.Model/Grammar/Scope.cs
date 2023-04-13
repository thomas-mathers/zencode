using System.Text;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Model.Grammar;

public record Scope : AstNode
{
    public IReadOnlyList<Statement> Statements { get; init; } = Array.Empty<Statement>();

    public Scope()
    {
        
    }

    public Scope(params Statement[] statements)
    {
        Statements = statements;
    }
    
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
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("{");

        foreach (var statement in Statements)
        {
            stringBuilder.Append("    ");
            stringBuilder.Append(statement);
            stringBuilder.AppendLine();
        }

        stringBuilder.Append('}');

        return stringBuilder.ToString();
    }
}

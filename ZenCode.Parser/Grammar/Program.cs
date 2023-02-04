using ZenCode.Parser.Grammar.Statements;

namespace ZenCode.Parser.Grammar;

public struct Program : IEquatable<Program>
{
    public IReadOnlyList<Statement> Statements { get; }

    public Program(IReadOnlyList<Statement> statements)
    {
        Statements = statements;
    }

    public bool Equals(Program other)
    {
        if (Statements.Count != other.Statements.Count)
        {
            return false;
        }

        for (var i = 0; i < Statements.Count; i++)
        {
            if (!Statements[i].Equals(other.Statements[i]))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is Program other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Statements.GetHashCode();
    }
}

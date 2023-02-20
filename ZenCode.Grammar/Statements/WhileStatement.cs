using ZenCode.Grammar.Expressions;

namespace ZenCode.Grammar.Statements;

public record WhileStatement(Expression ConditionExpression) : Statement
{
    public IReadOnlyList<Statement> Statements { get; init; } = Array.Empty<Statement>();

    public virtual bool Equals(WhileStatement? other)
    {
        return other != null && ConditionExpression.Equals(other.ConditionExpression) && Statements.SequenceEqual(other.Statements);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), ConditionExpression, Statements);
    }
}
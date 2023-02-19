using ZenCode.Grammar.Expressions;

namespace ZenCode.Grammar.Statements;

public record IfStatement(Expression ConditionExpression, IReadOnlyList<Statement> Statements) : Statement
{
    public virtual bool Equals(IfStatement? other)
    {
        return other != null && ConditionExpression.Equals(other.ConditionExpression) && Statements.SequenceEqual(other.Statements);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), ConditionExpression, Statements);
    }
}
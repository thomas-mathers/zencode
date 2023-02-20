namespace ZenCode.Grammar.Expressions;

public record FunctionCall : Expression
{
    public required VariableReferenceExpression VariableReferenceExpression { get; init; }
    public IReadOnlyList<Expression> Parameters { get; init; } = Array.Empty<Expression>();

    public virtual bool Equals(FunctionCall? other)
    {
        return other != null && VariableReferenceExpression.Equals(other.VariableReferenceExpression) &&
               Parameters.SequenceEqual(other.Parameters);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), VariableReferenceExpression, Parameters);
    }
}